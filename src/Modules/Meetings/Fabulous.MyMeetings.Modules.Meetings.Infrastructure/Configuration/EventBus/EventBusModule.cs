using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.EventBus;

internal static class EventBusModule
{
    public static void AddEventBus(this IServiceCollection services, IEventBus eventBus)
    {
        services.AddScoped<IEventBus>(_ => eventBus);
        services.AddHostedService<EventBusHostedService>();
    }
}