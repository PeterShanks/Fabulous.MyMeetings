namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposalAcceptedDomainEvent(MeetingGroupProposalId meetingGroupProposalId) : DomainEvent
{
    public MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;
}