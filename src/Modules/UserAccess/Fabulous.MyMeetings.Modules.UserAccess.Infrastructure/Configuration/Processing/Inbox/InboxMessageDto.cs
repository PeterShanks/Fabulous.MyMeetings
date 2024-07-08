namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;

public class InboxMessageDto
{
    public required Guid Id { get; set; }

    public required string Type { get; set; }

    public required string Data { get; set; }
}