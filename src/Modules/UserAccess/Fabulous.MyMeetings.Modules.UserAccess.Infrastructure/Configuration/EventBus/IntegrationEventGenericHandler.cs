using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus;

internal class IntegrationEventGenericHandler<T> : IIntegrationEventHandler<T>
    where T : IntegrationEvent
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public IntegrationEventGenericHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Handle(T @event)
    {
        using var connection = _sqlConnectionFactory.GetOpenConnection();

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

        await connection.ExecuteScalarAsync(sql, new
        {
            @event.Id,
            @event.OccurredOn,
            Type = @event.GetType().FullName,
            Data = json
        });
    }
}