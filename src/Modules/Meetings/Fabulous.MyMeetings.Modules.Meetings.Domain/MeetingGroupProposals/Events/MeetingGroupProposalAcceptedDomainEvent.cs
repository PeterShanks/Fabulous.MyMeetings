using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposalAcceptedDomainEvent(MeetingGroupProposalId meetingGroupProposalId) : DomainEvent
{
    public MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;
}