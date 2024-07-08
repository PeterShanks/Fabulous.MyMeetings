using System.Text.Json;
using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Domain;
using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IDomainEventNotificationFactory _domainEventNotificationFactory;
    private readonly IDomainEventsAccessor _domainEventsAccessor;
    private readonly IDomainNotificationsMapper _domainNotificationsMapper;
    private readonly IMediator _mediator;
    private readonly IOutbox _outbox;

    public DomainEventsDispatcher(
        IMediator mediator,
        IOutbox outbox,
        IDomainEventsAccessor domainEventsAccessor,
        IDomainNotificationsMapper domainNotificationsMapper,
        IDomainEventNotificationFactory domainEventNotificationFactory)
    {
        _mediator = mediator;
        _outbox = outbox;
        _domainEventsAccessor = domainEventsAccessor;
        _domainNotificationsMapper = domainNotificationsMapper;
        _domainEventNotificationFactory = domainEventNotificationFactory;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEvents = _domainEventsAccessor.GetAllDomainEvents();

        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

        foreach (var domainEvent in domainEvents)
        {
            var domainNotification =
                _domainEventNotificationFactory.Create(domainEvent);

            if (domainNotification != null)
                domainEventNotifications.Add(domainNotification);
        }

        _domainEventsAccessor.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent);

        foreach (var domainEventNotification in domainEventNotifications)
        {
            var typeName = _domainNotificationsMapper.GetName(domainEventNotification.GetType());
            var data = JsonSerializer.Serialize(domainEventNotification, JsonOptions);

            var outboxMessage = new OutboxMessage(
                domainEventNotification.Id,
                domainEventNotification.DomainEvent.OccurredOn,
                typeName!,
                data
            );

            _outbox.Add(outboxMessage);
        }
    }
}