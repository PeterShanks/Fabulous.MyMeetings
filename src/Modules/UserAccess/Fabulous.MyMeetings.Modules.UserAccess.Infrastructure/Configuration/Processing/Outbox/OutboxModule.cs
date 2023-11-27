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
        public static void AddOutbox(this IServiceCollection services, BiDictionary<string, Type> domainNotificationsMap)
        {
            services.AddScoped<IOutbox, OutboxAccessor>();

            CheckMappings(domainNotificationsMap);

            services.AddSingleton<IDomainNotificationsMapper>(_ =>
                new DomainNotificationsMapper(domainNotificationsMap));
        }

        private static void CheckMappings(BiDictionary<string, Type> domainNotificationsMap)
        {
            var domainEventNotifications = Assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IDomainEventNotification)));

            var unmappedNotifications = new List<Type>();

            foreach (var domainEventNotification in domainEventNotifications)
                if (!domainNotificationsMap.TryGetBySecond(domainEventNotification, out var name))
                    unmappedNotifications.Add(domainEventNotification);

            if (unmappedNotifications.Count > 0)
            {
                var unmappedNotificationNames = unmappedNotifications
                    .Select(x => x.FullName)
                    .Aggregate((acc, cur) => string.Join(',', acc, cur));


                throw new ApplicationException($"Domain Event Notifications {unmappedNotificationNames} are not mapped");
            }
        }
    }
}
