using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.EventBus;

internal class EventBusHostedService(
    IEventBus eventBus, 
    ILogger<EventBusHostedService> logger,
    ISqlConnectionFactory sqlConnectionFactory,
    JsonSerializerOptions jsonSerializerOptions) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting event bus hosting service");
        return Task.CompletedTask;
    }

    private void SubscribeToIntegrationEvent<T>()
        where T : IntegrationEvent
    {
        logger.LogInformation("Subscribed to {IntegrationEvent}", typeof(T).FullName);
        eventBus.Subscribe(new IntegrationEventGenericHandler<T>(sqlConnectionFactory, jsonSerializerOptions));
    }
}