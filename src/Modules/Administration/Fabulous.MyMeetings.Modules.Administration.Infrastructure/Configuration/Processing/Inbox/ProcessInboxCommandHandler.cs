using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox;

internal class ProcessInboxCommandHandler(
    IMediator mediator,
    ISqlConnectionFactory sqlConnectionFactory,
    TimeProvider timeProvider,
    JsonSerializerOptions jsonSerializerOptions) : ICommandHandler<ProcessInboxCommand>
{
    public async Task Handle(ProcessInboxCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
                Type,
                Data
            FROM Administration.InboxMessages
            WHERE ProcessedDate IS NULL
            ORDER BY OccurredOn
            """;

        var messages = await connection.QueryAsync<InboxMessageDto>(sql);

        const string updateProcessedDateSql =
            """
            UPDATE Administration.InboxMessages
                SET ProcessedDate = @Date
            WHERE Id = @Id
            """;

        foreach (var message in messages)
        {
            var type = message.Type.GetTypeByName();

            if (JsonSerializer.Deserialize(message.Data, type!, jsonSerializerOptions) is not INotification
                notification)
                throw new InvalidOperationException("Couldn't create notification object");

            await mediator.Publish(notification, cancellationToken);

            await connection.ExecuteAsync(updateProcessedDateSql, new
            {
                Date = timeProvider.GetUtcNow().UtcDateTime,
                message.Id
            });
        }
    }
}