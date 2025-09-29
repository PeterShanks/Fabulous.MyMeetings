using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using MediatR;
using Polly;
using Polly.Registry;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing.InternalCommands;

internal class ProcessInternalCommandsCommandHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IInternalCommandsMapper internalCommandsMapper,
    IMediator mediator,
    ResiliencePipelineProvider<string> resiliencePipelineProvider,
    TimeProvider timeProvider,
    JsonSerializerOptions jsonSerializerOptions) : ICommandHandler<ProcessInternalCommandsCommand>
{
    public async Task Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
                Type,
                Data
            FROM UserRegistrations.InternalCommands
            WHERE ProcessedDate IS NULL
            ORDER BY EnqueueDate
            """;

        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var pipeline = resiliencePipelineProvider.GetPipeline(PollyPolicies.WaitAndRetry);
        var context = ResilienceContextPool.Shared.Get(cancellationToken);

        foreach (var command in commands)
            try
            {
                await pipeline.ExecuteAsync(
                    async (ctx, state) => await ProcessCommand(state.command, state.cancellationToken),
                    context,
                    (command, cancellationToken)
                );
            }
            catch (Exception e)
            {
                await connection.ExecuteScalarAsync(
                    """
                    UPDATE UserRegistrations.InternalCommands
                        SET ProcessedDate = @Date,
                            Error = @Error
                    WHERE Id = @Id
                    """,
                    new
                    {
                        Date = timeProvider.GetUtcNow().UtcDateTime,
                        Error = e.ToString(),
                        command.Id
                    });
            }
    }

    private Task ProcessCommand(InternalCommandDto commandDto, CancellationToken cancellationToken)
    {
        var command = JsonSerializer.Deserialize(
            commandDto.Data, 
            internalCommandsMapper.GetType(commandDto.Type)!, 
            jsonSerializerOptions);

        if (command is null)
            throw new InvalidOperationException($"Couldn't deserialize into type {commandDto.Type}");

        return mediator.Send(command, cancellationToken);
    }

    private class InternalCommandDto
    {
        public required Guid Id { get; init; }

        public required string Type { get; init; }

        public required string Data { get; init; }
    }
}