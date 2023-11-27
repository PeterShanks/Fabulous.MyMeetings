using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus
{
    internal class EventBusHostedService : BackgroundService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        public EventBusHostedService(IEventBus eventBus, ILogger logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting event bus hosting service");
            return Task.CompletedTask;
        }

        private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus, ILogger logger)
            where T : IntegrationEvent
        {
            logger.LogInformation("Subscribed to {IntegrationEvent}", typeof(T).FullName);
            eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
        }
    }
}
