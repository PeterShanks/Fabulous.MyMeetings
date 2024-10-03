namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Processing.Outbox;

public class OutboxMessageDto
{
    public required Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Data { get; set; }
}