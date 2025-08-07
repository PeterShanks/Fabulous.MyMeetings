using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;
using Fabulous.MyMeetings.Modules.UserRegistrations.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventBus;

internal class EventBusHostedService(
    IEventBus eventBus, 
    ILogger logger, 
    ISqlConnectionFactory sqlConnectionFactory,
    JsonSerializerOptions jsonSerializerOptions) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting event bus hosting service");

        SubscribeToIntegrationEvent<MeetingGroupProposedIntegrationEvent>();
        SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>();

        return Task.CompletedTask;
    }

    private void SubscribeToIntegrationEvent<T>()
        where T : IntegrationEvent
    {
        logger.LogInformation("Subscribed to {IntegrationEvent}", typeof(T).FullName);
        eventBus.Subscribe(new IntegrationEventGenericHandler<T>(sqlConnectionFactory, jsonSerializerOptions));
    }
}