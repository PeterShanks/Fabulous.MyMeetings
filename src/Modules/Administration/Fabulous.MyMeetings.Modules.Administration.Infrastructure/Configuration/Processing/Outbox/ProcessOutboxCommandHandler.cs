using System.Text.Json;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox;

internal class ProcessOutboxCommandHandler(
    IMediator mediator, 
    ISqlConnectionFactory sqlConnectionFactory,
    IDomainNotificationsMapper domainNotificationsMapper, 
    ILogger<ProcessOutboxCommandHandler> logger,
    TimeProvider timeProvider,
    JsonSerializerOptions jsonSerializerOptions) : ICommandHandler<ProcessOutboxCommand>
{
    private readonly ILogger _logger = logger;

    /// <exception cref="InvalidOperationException">When deserialization fails to destination type.</exception>
    public async Task Handle(ProcessOutboxCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();
        const string sql =
            """
            SELECT
                Id,
                Type,
                Data
            FROM Administration.OutboxMessages
            WHERE ProcessedDate IS NULL
            ORDER BY OccurredOn
            """;

        var messages = await connection.QueryAsync<OutboxMessageDto>(sql);

        const string updateProcessedDateSql =
            """
            UPDATE Administration.OutboxMessages
                SET ProcessedDate = @Date
            WHERE Id = @Id
            """;

        foreach (var message in messages)
        {
            var type = domainNotificationsMapper.GetType(message.Type);
            var notification =
                JsonSerializer.Deserialize(message.Data, type!, jsonSerializerOptions) as
                    IDomainEventNotification;

            if (notification is null)
                throw new InvalidOperationException($"Couldn't deserialize into type {message.Type}");

            using (_logger.BeginScope(new List<KeyValuePair<string, object>>
                       { new("Context", message.Id) }))
            {
                await mediator.Publish(notification, cancellationToken);
                await connection.ExecuteScalarAsync(updateProcessedDateSql, new
                {
                    Date = timeProvider.GetUtcNow().UtcDateTime,
                    message.Id
                });
            }
        }
    }
}