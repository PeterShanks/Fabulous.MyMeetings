using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    public sealed class InMemoryEventBus: IEventBus
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlersDictionary =
            new Dictionary<string, List<IIntegrationEventHandler>>();

        public InMemoryEventBus(ILogger<InMemoryEventBus> logger)
        {
            _logger = logger;
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent
        {
            var eventType = typeof(T).FullName;

            if (eventType == null) return;

            if (_handlersDictionary.TryGetValue(eventType, out var handlers))
            {
                handlers.Add(handler);
            }
            else
            {
                _handlersDictionary.Add(eventType, new() { handler });
            }

            _logger.LogInformation("{HandlerType} was registered as one of {EventType} integration event handlers", handler.GetType(), eventType);
        }

        public async Task Publish<T>(T @event)
            where T : IntegrationEvent
        {
            _logger.LogInformation("Publishing {Event}", @event.GetType().FullName);

            var eventType = typeof(T).FullName;

            if (eventType == null) return;

            var handlers = _handlersDictionary[eventType];

            foreach (var integrationEventHandler in handlers)
            {
                if (integrationEventHandler is IIntegrationEventHandler<T> handler)
                {
                    await handler.Handle(@event);
                }
            }
        }
    }
}
