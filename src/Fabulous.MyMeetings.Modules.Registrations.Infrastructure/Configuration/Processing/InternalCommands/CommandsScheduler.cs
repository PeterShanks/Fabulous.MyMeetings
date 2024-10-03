using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.InternalCommands;

internal class CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory) : ICommandsScheduler
{
    public Task EnqueueAsync(InternalCommand command)
    {
        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });
    }

    public Task EnqueueAsync<T>(InternalCommand<T> command)
    {
        return AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });
    }

    private Task AddToInternalCommands(object command)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert =
            """
            INSERT INTO Registrations.InternalCommands (
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