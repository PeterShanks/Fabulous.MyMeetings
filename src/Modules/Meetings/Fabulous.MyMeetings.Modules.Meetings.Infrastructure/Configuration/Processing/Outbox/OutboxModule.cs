using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;

internal static class OutboxModule
{
    public static void AddOutbox(this IServiceCollection services)
    {
        services.AddScoped<IOutbox, OutboxAccessor>();
    }
}