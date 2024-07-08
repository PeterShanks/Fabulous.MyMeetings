using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public interface IDomainEventNotificationFactory
{
    IDomainEventNotification<TDomainEvent>? Create<TDomainEvent>(
        TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
}