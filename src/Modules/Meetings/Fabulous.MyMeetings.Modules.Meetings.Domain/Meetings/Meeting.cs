using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class Meeting : Entity, IAggregateRoot
{
    private readonly List<MeetingAttendee> _attendees = [];
    private readonly List<MeetingNotAttendee> _notAttendees = [];
    private readonly List<MeetingWaitlistMember> _waitlistMembers = [];

    public MeetingId Id { get; }
    public MeetingGroupId MeetingGroupId { get; }
    public string Title { get; private set; }
    public MeetingTerm Term { get; private set; }
    public string Description { get; private set; }
    public MeetingLocation Location { get; private set; }
    public MeetingLimits MeetingLimits { get; private set; }
    public Term RsvpTerm { get; private set; }
    public MoneyValue EventFee { get; private set; }
    public MemberId CreatorId { get; }
    public DateTime CreatedDate { get; }
    public MemberId? ChangeMemberId { get; private set; }
    public DateTime? ChangeDate { get; private set; }
    public DateTime? CancelDate { get; private set; }
    public MemberId? CancelMemberId { get; private set; }
    public bool IsCanceled { get; private set; }
    public IReadOnlyCollection<MeetingAttendee> Attendees => _attendees.AsReadOnly();
    public IReadOnlyCollection<MeetingNotAttendee> NotAttendees => _notAttendees.AsReadOnly();
    public IReadOnlyCollection<MeetingWaitlistMember> WaitlistMembers => _waitlistMembers.AsReadOnly();

    private Meeting()
    {
        // Only for EF.
    }

    internal static Meeting CreateNew(
        MeetingGroupId meetingGroupId,
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        MeetingLimits meetingLimits,
        Term rsvpTerm,
        MoneyValue eventFee,
        List<MemberId> hostsMembersIds,
        MemberId creatorId)
    {
        return new Meeting(
            meetingGroupId,
            title,
            term,
            description,
            location,
            meetingLimits,
            rsvpTerm,
            eventFee,
            hostsMembersIds,
            creatorId);
    }

    private Meeting(
        MeetingGroupId meetingGroupId,
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        MeetingLimits meetingLimits,
        Term rsvpTerm,
        MoneyValue eventFee,
        List<MemberId> hostsMemberIds,
        MemberId creatorId)
    {
        Id = new MeetingId(Guid.CreateVersion7());
        MeetingGroupId = meetingGroupId;
        Title = title;
        Term = term;
        Description = description;
        Location = location;
        MeetingLimits = meetingLimits;
        SetRsvpTerm(rsvpTerm, Term);
        EventFee = eventFee;
        CreatorId = creatorId;
        CreatedDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingCreatedDomainEvent(Id));
        var rsvpDate = DateTime.UtcNow;
        if (hostsMemberIds.Count > 0)
        {
            foreach (var hostsMemberId in hostsMemberIds)
            {
                _attendees.Add(MeetingAttendee.CreateNew(Id, hostsMemberId, rsvpDate, MeetingAttendeeRole.Host, 0, MoneyValue.Undefined));
            }
        }
        else
        {
            _attendees.Add(MeetingAttendee.CreateNew(Id, creatorId, rsvpDate, MeetingAttendeeRole.Host, 0, MoneyValue.Undefined));
        }
    }

    public void ChangeMainAttributes(
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        MeetingLimits meetingLimits,
        Term rsvpTerm,
        MoneyValue eventFee,
        MemberId modifyMemberId)
    {
        CheckRule(new AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule(
            MeetingLimits,
            GetAllActiveAttendeesWithGuestsNumber()));

        Title = title;
        Term = term;
        Description = description;
        Location = location;
        MeetingLimits = meetingLimits;
        SetRsvpTerm(rsvpTerm, Term);
        EventFee = eventFee;
        ChangeMemberId = modifyMemberId;
        ChangeDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingMainAttributesChangedDomainEvent(Id));
    }

    public void AddAttendee(MeetingGroup meetingGroup, MemberId attendeeId, int guestsNumber)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new AttendeeCanBeAddedOnlyInRsvpTermRule(RsvpTerm));
        CheckRule(new MeetingAttendeeMustBeAMemberOfGroupRule(attendeeId, meetingGroup));
        CheckRule(new MemberCannotBeAnAttendeeOfMeetingMoreThanOnceRule(attendeeId, _attendees));
        CheckRule(new MeetingGuestsNumberIsAboveLimitRule(MeetingLimits.GuestsLimit, guestsNumber));
        CheckRule(new MeetingAttendeesNumberIsAboveLimitRule(MeetingLimits.AttendeesLimit, GetAllActiveAttendeesWithGuestsNumber(), guestsNumber));

        var notAttendee = GetActiveNotAttendee(attendeeId);
        notAttendee?.ChangeDecision();

        _attendees.Add(MeetingAttendee.CreateNew(
            Id,
            attendeeId,
            DateTime.UtcNow,
            MeetingAttendeeRole.Attendee, 
            guestsNumber,
            EventFee));
    }

    public void AddNotAttendee(MemberId memberId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new MemberCannotBeNotAttendeeTwiceRule(_notAttendees, memberId));

        _notAttendees.Add(MeetingNotAttendee.CreateNew(Id, memberId));

        var attendee = GetActiveAttendee(memberId);
        attendee?.ChangeDecision();

        var nextWaitlistMember = _waitlistMembers
            .Where(x => x.IsActive())
            .OrderBy(x => x.SignUpDate)
            .FirstOrDefault();

        if (nextWaitlistMember != null)
        {
            _attendees.Add(MeetingAttendee.CreateNew(
                Id,
                nextWaitlistMember.MemberId,
                nextWaitlistMember.SignUpDate,
                MeetingAttendeeRole.Attendee,
                0,
                EventFee));
            nextWaitlistMember.MarkIsMovedToAttendees();
        }
    }

    public void ChangeNotAttendeeDecision(MemberId memberId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new NotActiveNotAttendeeCannotChangeDecisionRule(_notAttendees, memberId));

        var notAttendee = _notAttendees.Single(a => a.IsActiveNotAttendee(memberId));
        notAttendee.ChangeDecision();
    }

    public void SignUpMemberToWaitlist(MeetingGroup meetingGroup, MemberId memberId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new AttendeeCanBeAddedOnlyInRsvpTermRule(RsvpTerm));
        CheckRule(new MemberOnWaitlistMustBeAMemberOfGroupRule(meetingGroup, memberId, _attendees));
        CheckRule(new MemberCannotBeMoreThanOnceOnMeetingWaitlistRule(_waitlistMembers, memberId));

        _waitlistMembers.Add(MeetingWaitlistMember.CreateNew(Id, memberId));
    }

    public void SignOffMemberFromWaitlist(MemberId memberId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new NotActiveMemberOfWaitlistCannotBeSignedOffRule(_waitlistMembers, memberId));

        var memberOnWaitlist = GetActiveMemberOnWaitlist(memberId);
        memberOnWaitlist.SignOff();
    }

    public void SetHostRole(MeetingGroup meetingGroup, MemberId settingMemberId, MemberId newOrganizerId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(settingMemberId, meetingGroup, _attendees));
        CheckRule(new OnlyMeetingAttendeeCanHaveChangedRoleRule(_attendees, newOrganizerId));

        var attendee = GetActiveAttendee(newOrganizerId) ;
        attendee!.SetAsHost();
    }

    public void SetAttendeeRole(MeetingGroup meetingGroup, MemberId settingMemberId, MemberId attendeeId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new OnlyMeetingOrGroupOrganizerCanSetMeetingMemberRolesRule(settingMemberId, meetingGroup, _attendees));
        CheckRule(new OnlyMeetingAttendeeCanHaveChangedRoleRule(_attendees, attendeeId));

        var attendee = GetActiveAttendee(attendeeId);
        attendee!.SetAsAttendee();

        var meetingHostNumber = _attendees.Count(x => x.IsActiveHost());
        CheckRule(new MeetingMustHaveAtLeastOneHostRule(meetingHostNumber));
    }

    public void Cancel(MemberId cancelMemberId)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));

        if (IsCanceled) return;

        IsCanceled = true;
        CancelDate = DateTime.UtcNow;
        CancelMemberId = cancelMemberId;

        AddDomainEvent(new MeetingCanceledDomainEvent(Id, CancelMemberId, CancelDate.Value));
    }

    public void RemoveAttendee(MemberId attendeeId, MemberId removingPersonId, string reason)
    {
        CheckRule(new MeetingCannotBeChangedAfterStartRule(Term));
        CheckRule(new OnlyActiveAttendeeCanBeRemovedFromMeetingRule(_attendees, attendeeId));

        var attendee = GetActiveAttendee(attendeeId);

        attendee!.Remove(removingPersonId, reason);
    }

    public void MarkAttendeeFeeAsPaid(MemberId memberId)
    {
        var attendee = GetActiveAttendee(memberId);
        attendee!.MarkFeeAsPaid();
    }

    public MeetingComment AddComment(MemberId authorId, string comment, MeetingGroup meetingGroup, MeetingCommentingConfiguration meetingCommentingConfiguration)
        => MeetingComment.Create(
            Id,
            authorId, 
            comment, 
            meetingGroup,
            meetingCommentingConfiguration);

    public MeetingCommentingConfiguration CreateCommentingConfiguration() => MeetingCommentingConfiguration.Create(Id);

    private MeetingWaitlistMember GetActiveMemberOnWaitlist(MemberId memberId)
    {
        return _waitlistMembers.Single(x => x.IsActiveOnWaitlist(memberId));
    }

    private MeetingAttendee? GetActiveAttendee(MemberId memberId)
    {
        return _attendees.SingleOrDefault(x => x.IsActiveAttendee(memberId));
    }

    private MeetingNotAttendee? GetActiveNotAttendee(MemberId memberId)
    {
        return _notAttendees.SingleOrDefault(x => x.IsActiveNotAttendee(memberId));
    }

    private int GetAllActiveAttendeesWithGuestsNumber()
    {
        return _attendees
            .Where(a => a.IsActive())
            .Sum(a => a.GetAttendeeWithGuestsNumber());
    }

    private void SetRsvpTerm(Term rsvpTerm, MeetingTerm meetingTerm)
    {
        if (!rsvpTerm.EndDate.HasValue || rsvpTerm.EndDate.Value > meetingTerm.EndDate)
        {
            RsvpTerm = Meetings.Term.CreateNewBetweenDates(rsvpTerm.StartDate, meetingTerm.StartDate);
        }
        else
        {
            RsvpTerm = rsvpTerm;
        }
    }
}