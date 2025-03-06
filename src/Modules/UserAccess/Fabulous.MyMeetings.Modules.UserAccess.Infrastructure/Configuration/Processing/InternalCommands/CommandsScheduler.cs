using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using System.Text.Json;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;

internal class CommandsScheduler(
    ISqlConnectionFactory sqlConnectionFactory,
    TimeProvider timeProvider) : ICommandsScheduler
{
    public Task EnqueueAsync(InternalCommand command)
    {
        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = timeProvider.GetUtcNow().UtcDateTime,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });
    }

    public Task EnqueueAsync<T>(InternalCommand<T> command)
    {
        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = timeProvider.GetUtcNow().UtcDateTime,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });
    }

    private Task AddToInternalCommands(object command)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert =
            """
            INSERT INTO Users.InternalCommands (
                Id,
                EnqueueDate,
                Type,
                Data
            ) VALUES (
                @Id,
                @EnqueueDate,
                @Type,
                @Data
            )
            """;

        return connection.ExecuteAsync(sqlInsert, command);
    }
}