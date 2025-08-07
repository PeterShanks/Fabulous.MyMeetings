using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Domain;
using MediatR;
using System.Text.Json;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public class DomainEventsDispatcher(
    IMediator mediator,
    IOutbox outbox,
    IDomainEventsAccessor domainEventsAccessor,
    IDomainNotificationsMapper domainNotificationsMapper,
    IDomainEventNotificationFactory domainEventNotificationFactory,
    JsonSerializerOptions jsonSerializerOptions) : IDomainEventsDispatcher
{
    public async Task DispatchEventsAsync()
    {
        var domainEvents = domainEventsAccessor.GetAllDomainEvents();

        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

        foreach (var domainEvent in domainEvents)
        {
            var domainNotification =
                domainEventNotificationFactory.Create(domainEvent);

            if (domainNotification != null)
                domainEventNotifications.Add(domainNotification);
        }

        domainEventsAccessor.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);

        foreach (var domainEventNotification in domainEventNotifications)
        {
            var domainEventNotificationType = domainEventNotification.GetType();
            var typeName = domainNotificationsMapper.GetName(domainEventNotificationType);
            var data = JsonSerializer.Serialize(
                domainEventNotification, 
                domainEventNotificationType, 
                jsonSerializerOptions);

            var outboxMessage = new OutboxMessage(
                domainEventNotification.Id,
                domainEventNotification.DomainEvent.OccurredOn,
                typeName!,
                data
            );

            outbox.Add(outboxMessage);
        }
    }
}