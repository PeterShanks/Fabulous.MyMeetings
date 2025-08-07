using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;

public class MeetingGroupProposalAcceptedIntegrationEvent(
    Guid id,
    DateTime occurredOn,
    Guid meetingGroupProposalId) : IntegrationEvent(id, occurredOn)
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
}