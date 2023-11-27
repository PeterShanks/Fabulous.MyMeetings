using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;


namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus
{
    internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        public Task Handle(T @event)
        {
            using var scope = CompositionRoot.BeginScope();
            using var connection =
                scope.ServiceProvider
                    .GetRequiredService<ISqlConnectionFactory>()
                    .GetOpenConnection();

            var json = JsonSerializer.Serialize(@event, JsonSerializerOptionsInstance);

            const string sql =
                """
                INSERT INTO Users.InboxMessages (
                    Id,
                    OccurredOn,
                    Type,
                    Data
                ) VALUES (
                    @Id,
                    @OccurredOn,
                    @Type,
                    @Data
                )
                """;

            return connection.ExecuteScalarAsync(sql, new
            {
                @event.Id,
                @event.OccurredOn,
                Type = @event.GetType().FullName,
                Data = json
            });
        }
    }
}
