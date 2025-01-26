using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing.Outbox;

internal static class OutboxModule
{
    public static void AddOutbox(this IServiceCollection services)
    {
        services.AddScoped<IOutbox, OutboxAccessor>();
    }
}