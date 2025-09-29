using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingAttendee: Entity
{
    public MemberId AttendeeId { get; }
    public MeetingId MeetingId { get; }
    public DateTime DecisionDate { get; }
    public MeetingAttendeeRole Role { get; private set; }
    public int GuestsNumber { get; }
    public bool DecisionChanged { get; private set; }
    public DateTime? DecisionChangedDate { get; private set; }
    public DateTime RemovedDate { get; private set; }
    public MemberId? RemovingMemberId { get; private set; }
    public string? RemovingReason { get; private set; }
    public bool IsRemoved { get; private set; }
    public MoneyValue Fee { get;  }
    public bool IsFeedPaid { get; private set; }

    private MeetingAttendee()
    {
        // Only for EF
    }

    internal static MeetingAttendee CreateNew(
        MeetingId meetingId,
        MemberId attendeeId,
        DateTime decisionDate,
        MeetingAttendeeRole role,
        int guestsNumber,
        MoneyValue eventFee)
    {
        return new MeetingAttendee(meetingId, attendeeId, decisionDate, role, guestsNumber, eventFee);
    }

    private MeetingAttendee(
        MeetingId meetingId,
        MemberId attendeeId,
        DateTime decisionDate,
        MeetingAttendeeRole role,
        int guestsNumber,
        MoneyValue eventFee)
    {
        AttendeeId = attendeeId;
        MeetingId = meetingId;
        DecisionDate = decisionDate;
        Role = role;
        GuestsNumber = guestsNumber;
        DecisionChanged = false;
        IsFeedPaid = false;
            
        Fee = eventFee == MoneyValue.Undefined
            ? MoneyValue.Undefined
            : (1 + guestsNumber) * eventFee;

        AddDomainEvent(new MeetingAttendeeAddedDomainEvent(
            MeetingId,
            AttendeeId,
            DecisionDate,
            Role.Value,
            GuestsNumber,
            Fee.Value,
            Fee.Currency));
    }

    internal void ChangeDecision()
    {
        DecisionChanged = true;
        DecisionChangedDate = DateTime.UtcNow;

        AddDomainEvent(new MeetingAttendeeChangedDecisionDomainEvent(AttendeeId, MeetingId));
    }

    internal bool IsActiveAttendee(MemberId memberId)
    {
        return AttendeeId == memberId && !IsRemoved;
    }

    internal bool IsActive() 
        => !DecisionChangedDate.HasValue && !IsRemoved;

    internal bool IsActiveHost()
        => IsActive() && Role == MeetingAttendeeRole.Host;

    internal int GetAttendeeWithGuestsNumber()
        => GuestsNumber + 1;

    internal void SetAsHost()
    {
        Role = MeetingAttendeeRole.Host;

        AddDomainEvent(new NewMeetingHostSetDomainEvent(MeetingId, AttendeeId));
    }

    internal void SetAsAttendee()
    {
        CheckRule(new MemberCannotHaveSetAttendeeRoleMoreThanOnceRule(Role));

        Role = MeetingAttendeeRole.Attendee;

        AddDomainEvent(new MemberSetAsAttendeeDomainEvent(MeetingId, AttendeeId));
    }

    internal void Remove(MemberId removingMemberId, string reason)
    {
        CheckRule(new ReasonOfRemovingAttendeeFromMeetingMustBeProvidedRule(reason));

        IsRemoved = true;
        RemovedDate = DateTime.UtcNow;
        RemovingMemberId = removingMemberId;
        RemovingReason = reason;

        AddDomainEvent(new MeetingAttendeeRemovedDomainEvent(AttendeeId, MeetingId, reason));
    }

    internal void MarkFeeAsPaid()
    {
        IsFeedPaid = true;

        AddDomainEvent(new MeetingAttendeeFeePaidDomainEvent(MeetingId, AttendeeId));
    }
}