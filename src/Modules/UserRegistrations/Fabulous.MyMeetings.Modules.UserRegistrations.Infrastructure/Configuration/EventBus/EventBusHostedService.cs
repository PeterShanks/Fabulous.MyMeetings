using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.EventBus;

internal class EventBusHostedService(IEventBus eventBus, ILogger logger) : BackgroundService
{
    private readonly IEventBus _eventBus = eventBus;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting event bus hosting service");
        return Task.CompletedTask;
    }

    //private static void SubscribeToIntegrationEvent<T>(IEventBus eventBus, ILogger logger)
    //    where T : IntegrationEvent
    //{
    //    logger.LogInformation("Subscribed to {IntegrationEvent}", typeof(T).FullName);
    //    eventBus.Subscribe(new IntegrationEventGenericHandler<T>());
    //}
}