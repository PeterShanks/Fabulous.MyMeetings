using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;

internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
{
    private readonly IMediator _mediator;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ProcessInboxCommandHandler(
        IMediator mediator,
        ISqlConnectionFactory sqlConnectionFactory,
        ILogger<ProcessInboxCommandHandler> logger)
    {
        _mediator = mediator;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
                Type,
                Data
            FROM Users.InboxMessages
            WHERE ProcessedDate IS NULL
            ORDER BY OccurredOn
            """;

        var messages = await connection.QueryAsync<InboxMessageDto>(sql);

        const string updateProcessedDateSql =
            """
            UPDATE Users.InboxMessages
                SET ProcessedDate = @Date
            WHERE Id = @Id
            """;

        foreach (var message in messages)
        {
            var type = message.Type.GetTypeByName();

            if (JsonSerializer.Deserialize(message.Data, type!, JsonSerializerOptionsInstance) is not INotification
                notification)
                throw new InvalidOperationException("Couldn't create notification object");

            await _mediator.Publish(notification, cancellationToken);

            await connection.ExecuteAsync(updateProcessedDateSql, new
            {
                Date = DateTime.UtcNow,
                message.Id
            });
        }
    }
}