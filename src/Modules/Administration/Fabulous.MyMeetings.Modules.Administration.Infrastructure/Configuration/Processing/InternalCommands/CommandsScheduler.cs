using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;

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
            INSERT INTO Administration.InternalCommands (
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