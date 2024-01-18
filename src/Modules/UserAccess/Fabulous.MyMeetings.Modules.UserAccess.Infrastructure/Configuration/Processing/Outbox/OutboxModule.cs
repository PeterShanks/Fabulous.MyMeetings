using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox
{
    internal static class OutboxModule
    {
        public static void AddOutbox(this IServiceCollection services)
        {
            services.AddScoped<IOutbox, OutboxAccessor>();
        }
    }
}
