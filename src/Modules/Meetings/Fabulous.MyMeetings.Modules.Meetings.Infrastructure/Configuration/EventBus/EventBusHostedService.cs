using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Payments.IntegrationEvents;
using Fabulous.MyMeetings.Modules.UserRegistrations.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.EventBus;

internal class EventBusHostedService(
    IEventBus eventBus, 
    ILogger<EventBusHostedService> logger,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Starting event bus hosting service");
        SubscribeToIntegrationEvent<MeetingGroupProposalAcceptedIntegrationEvent>();
        SubscribeToIntegrationEvent<SubscriptionExpirationDateChangedIntegrationEvent>();
        SubscribeToIntegrationEvent<NewUserRegisteredIntegrationEvent>();
        SubscribeToIntegrationEvent<MeetingFeePaidIntegrationEvent>();
        return Task.CompletedTask;
    }

    private void SubscribeToIntegrationEvent<T>()
        where T : IntegrationEvent
    {
        logger.LogInformation("Subscribed to {IntegrationEvent}", typeof(T).FullName);
        eventBus.Subscribe(new IntegrationEventGenericHandler<T>(serviceScopeFactory));
    }
}