using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using System.Text.Json;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;

internal class CommandsScheduler(
    ISqlConnectionFactory sqlConnectionFactory,
    IInternalCommandsMapper internalCommandsMapper,
    TimeProvider timeProvider,
    JsonSerializerOptions jsonSerializerOptions) : ICommandsScheduler
{
    public Task EnqueueAsync(InternalCommand command)
    {
        var commandType = command.GetType();

        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = timeProvider.GetUtcNow().UtcDateTime,
            Type = internalCommandsMapper.GetName(commandType),
            Data = JsonSerializer.Serialize(command, commandType, jsonSerializerOptions)
        });
    }

    public Task EnqueueAsync<T>(InternalCommand<T> command)
    {
        var commandType = command.GetType();

        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = timeProvider.GetUtcNow().UtcDateTime,
            Type = internalCommandsMapper.GetName(commandType),
            Data = JsonSerializer.Serialize(command, commandType, jsonSerializerOptions)
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