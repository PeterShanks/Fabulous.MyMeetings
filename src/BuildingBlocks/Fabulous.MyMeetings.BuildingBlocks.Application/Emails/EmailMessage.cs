namespace Fabulous.MyMeetings.BuildingBlocks.Application.Emails;

public readonly struct EmailMessage(
    string toAddress,
    string toName,
    string subject,
    string content)
{
    public string ToAddress { get; } = toAddress;

    public string ToName { get; } = toName;

    public string Subject { get; } = subject;

    public string Content { get; } = content;
}