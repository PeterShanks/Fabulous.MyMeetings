using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus;

internal static class EventBusModule
{
    public static void AddEventBus(this IServiceCollection services, IEventBus eventBus)
    {
        services.AddSingleton<IEventBus>(_ => eventBus);
        services.AddHostedService<EventBusHostedService>();
    }
}