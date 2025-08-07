namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposalVerificationRequestedDomainEvent(MeetingGroupProposalId meetingGroupProposalId)
    : DomainEvent
{
    public MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;
}