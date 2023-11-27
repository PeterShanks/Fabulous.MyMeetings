using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class CommandsScheduler : ICommandsScheduler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task EnqueueAsync(InternalCommand command) => AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });

        public Task EnqueueAsync<T>(InternalCommand<T> command) => AddToInternalCommands(new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonSerializer.Serialize(command, JsonSerializerOptionsInstance)
        });

        private Task AddToInternalCommands(object command)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

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
}
