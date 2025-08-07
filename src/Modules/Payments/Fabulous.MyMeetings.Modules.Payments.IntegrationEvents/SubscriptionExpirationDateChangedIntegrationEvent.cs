using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Payments.IntegrationEvents;

public class SubscriptionExpirationDateChangedIntegrationEvent(
    Guid id,
    DateTime occurredOn,
    Guid payerId,
    DateTime expirationDate): IntegrationEvent(id, occurredOn)
{
    public Guid PayerId { get; } = payerId;
    public DateTime ExpirationDate { get; } = expirationDate;
}