using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroup: Entity, IAggregateRoot
{
    public MeetingGroupId Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public MeetingGroupLocation Location { get; private set; }
    public MemberId CreatorId { get; }
    private readonly List<MeetingGroupMember> _members;
    public IReadOnlyCollection<MeetingGroupMember> Members => _members.AsReadOnly();
    public DateTime CreatedDate { get; }
    public DateTime? PaymentDateTo { get; private set; }

    internal static MeetingGroup CreateBasedOnProposal(
        MeetingGroupProposalId meetingGroupProposalId,
        string name,
        string description,
        MeetingGroupLocation location,
        MemberId creatorId) => new MeetingGroup(meetingGroupProposalId, name, description, location, creatorId);

    private MeetingGroup()
    {
        // Only for EF.
    }

    private MeetingGroup(MeetingGroupProposalId meetingGroupProposalId, string name, string description,
        MeetingGroupLocation location, MemberId creatorId)
    {
        Id = new MeetingGroupId(meetingGroupProposalId.Value);
        Name = name;
        Description = description;
        Location = location;
        CreatorId = creatorId;
        CreatedDate = DateTime.UtcNow;
        _members = [MeetingGroupMember.CreateMember(Id, creatorId, MeetingGroupMemberRole.Organizer)];

        AddDomainEvent(new MeetingGroupCreatedDomainEvent(Id, CreatorId));
    }

    public void EditGeneralAttributes(string name, string description, MeetingGroupLocation location)
    {
        Name = name;
        Description = description;
        Location = location;

        AddDomainEvent(new MeetingGroupGeneralAttributesEditedDomainEvent(Id, Name, Description, Location));
    }

    public void JoinToGroupMember(MemberId memberId)
    {
        CheckRule(new MeetingGroupMemberCannotBeAddedTwiceRule(Members, memberId));

        _members.Add(MeetingGroupMember.CreateMember(Id, memberId, MeetingGroupMemberRole.Member));
    }

    public void LeaveGroup(MemberId memberId)
    {
        CheckRule(new NotActualGroupMemberCannotLeaveGroupRule(Members, memberId));

        var member = _members.Single(x => x.MemberId == memberId);

        member.Leave();
    }

    public void SetExpirationDate(DateTime dateTo)
    {
        PaymentDateTo = dateTo;

        AddDomainEvent(new MeetingGroupPaymentInfoUpdatedDomainEvent(Id, PaymentDateTo.Value));
    }

    public Meeting CreateMeeting(
        string title,
        MeetingTerm term,
        string description,
        MeetingLocation location,
        int? attendeesLimit,
        int guestsLimit,
        Term rsvpTerm,
        MoneyValue eventFee,
        List<MemberId> hostsMembersIds,
        MemberId creatorId)
    {
        CheckRule(new MeetingCanBeOrganizedOnlyByPayedGroupRule(PaymentDateTo));
        CheckRule(new MeetingHostMustBeAMeetingGroupMemberRule(creatorId, hostsMembersIds, _members));

        return Meeting.CreateNew(
            Id,
            title,
            term,
            description,
            location,
            MeetingLimits.Create(attendeesLimit, guestsLimit),
            rsvpTerm, 
            eventFee, 
            hostsMembersIds, 
            creatorId);
    }

    internal bool IsMemberOfGroup(MemberId attendeeId) => Members.Any(x => x.IsMember(attendeeId));

    internal bool IsOrganizer(MemberId attendeeId) => Members.Any(x => x.IsOrganizer(attendeeId));
}