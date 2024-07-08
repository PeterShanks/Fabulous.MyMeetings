namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Emails;

public class EmailDto
{
    public required Guid Id { get; set; }

    public required string From { get; set; }

    public required string To { get; set; }

    public required string Subject { get; set; }

    public required string Content { get; set; }

    public required DateTime Date { get; set; }
}