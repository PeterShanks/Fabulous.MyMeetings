using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using MediatR;
using Polly;
using Polly.Registry;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;

internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
{
    private readonly IMediator _mediator;
    private readonly ResiliencePipelineProvider<string> _resiliencePipelineProvider;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ProcessInternalCommandsCommandHandler(ISqlConnectionFactory sqlConnectionFactory,
        IMediator mediator,
        ResiliencePipelineProvider<string> resiliencePipelineProvider)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _mediator = mediator;
        _resiliencePipelineProvider = resiliencePipelineProvider;
    }

    public async Task Handle(ProcessInternalCommandsCommand request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
                Type,
                Data
            FROM Users.InternalCommands
            WHERE ProcessedDate IS NULL
            ORDER BY EnqueueDate
            """;

        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var pipeline = _resiliencePipelineProvider.GetPipeline(PollyPolicies.WaitAndRetry);
        var context = ResilienceContextPool.Shared.Get();

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
                    UPDATE Users.InternalCommands
                        SET ProcessedDate = @Date,
                            Error = @Error
                    WHERE Id = @Id
                    """,
                    new
                    {
                        Date = DateTime.UtcNow,
                        Error = e.ToString(),
                        command.Id
                    });
            }
    }

    private Task ProcessCommand(InternalCommandDto commandDto, CancellationToken cancellationToken)
    {
        var type = commandDto.Type.GetTypeByName();
        var command = JsonSerializer.Deserialize(commandDto.Data, type!, JsonSerializerOptionsInstance);

        if (command is null)
            throw new InvalidOperationException($"Couldn't deserialize into type {commandDto.Type}");

        return _mediator.Send(command, cancellationToken);
    }

    private class InternalCommandDto
    {
        public required Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }
    }
}