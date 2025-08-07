using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.EventBus;

internal class IntegrationEventGenericHandler<T>(
    ISqlConnectionFactory sqlConnectionFactory,
    JsonSerializerOptions jsonSerializerOptions) : IIntegrationEventHandler<T>
    where T : IntegrationEvent
{
    public async Task Handle(T @event)
    {
        using var connection = sqlConnectionFactory.GetOpenConnection();

        var json = JsonSerializer.Serialize(@event, @event.GetType(), jsonSerializerOptions);

        const string sql =
            """
            INSERT INTO UserRegistrations.InboxMessages (
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

        await connection.ExecuteScalarAsync(sql, new
        {
            @event.Id,
            @event.OccurredOn,
            Type = @event.GetType().FullName,
            Data = json
        });
    }
}