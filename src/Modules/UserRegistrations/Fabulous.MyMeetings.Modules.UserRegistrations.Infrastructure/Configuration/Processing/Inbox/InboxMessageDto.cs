namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing.Inbox;

public class InboxMessageDto
{
    public required Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Data { get; set; }
}