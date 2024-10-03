using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration;

public class DomainEventNotificationFactory : IDomainEventNotificationFactory
{
    private static readonly Type DomainNotificationOpenGenericType = typeof(IDomainEventNotification<>);

    public IDomainEventNotification<TDomainEvent>? Create<TDomainEvent>(
        TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
    {
        var domainEventType = domainEvent.GetType();

        var type = AllTypes.SingleOrDefault(t =>
            t.IsClass &&
            t is { IsAbstract: false, BaseType: not null } &&
            t.BaseType.IsGenericType &&
            t.BaseType.GetGenericTypeDefinition() == DomainNotificationOpenGenericType &&
            t.BaseType.GenericTypeArguments[0] == domainEventType);

        return type is not null
            ? Activator.CreateInstance(type, domainEvent, domainEvent.Id) as IDomainEventNotification<TDomainEvent>
            : null;
    }
}