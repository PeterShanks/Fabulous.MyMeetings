using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Payments.IntegrationEvents;

public class MeetingFeePaidIntegrationEvent(
    Guid id,
    DateTime occurredOn,
    Guid payerId,
    Guid meetingId)
    : IntegrationEvent(id, occurredOn)
{
    public Guid PayerId { get; } = payerId;

    public Guid MeetingId { get; } = meetingId;
}