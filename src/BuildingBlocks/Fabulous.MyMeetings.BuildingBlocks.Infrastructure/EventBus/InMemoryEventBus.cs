using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

public sealed class InMemoryEventBus(ILogger<InMemoryEventBus> logger) : IEventBus
{
    private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlersDictionary = [];

    private readonly ILogger _logger = logger;

    public void Subscribe<THandler, TEvent>()
        where THandler : IIntegrationEventHandler<TEvent>
        where TEvent : IntegrationEvent
    {
        var eventType = typeof(T).FullName;

        if (eventType == null) return;

        if (_handlersDictionary.TryGetValue(eventType, out var handlers))
            handlers.Add(handler);
        else
            _handlersDictionary.Add(eventType, [handler]);

        _logger.LogInformation("{HandlerType} was registered as one of {EventType} integration event handlers",
            handler.GetType(), eventType);
    }

    public void Subscribe<T>(IIntegrationEventHandler<T> handler)
        where T : IntegrationEvent
    {
        var eventType = typeof(T).FullName;

        if (eventType == null) return;

        if (_handlersDictionary.TryGetValue(eventType, out var handlers))
            handlers.Add(handler);
        else
            _handlersDictionary.Add(eventType, [handler]);

        _logger.LogInformation("{HandlerType} was registered as one of {EventType} integration event handlers",
            handler.GetType(), eventType);
    }

    public async Task Publish<T>(T @event)
        where T : IntegrationEvent
    {
        _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);

        var eventType = typeof(T).FullName;

        if (eventType == null) return;

        if (!_handlersDictionary.TryGetValue(eventType, out var handlers))
            return;

        foreach (var integrationEventHandler in handlers)
            if (integrationEventHandler is IIntegrationEventHandler<T> handler)
                await handler.Handle(@event);
    }
}