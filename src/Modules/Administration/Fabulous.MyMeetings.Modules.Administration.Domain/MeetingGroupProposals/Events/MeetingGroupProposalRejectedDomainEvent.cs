namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposalRejectedDomainEvent(MeetingGroupProposalId meetingGroupProposalId) : DomainEvent
{
    internal MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;
}