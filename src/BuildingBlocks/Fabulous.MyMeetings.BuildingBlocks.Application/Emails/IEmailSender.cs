namespace Fabulous.MyMeetings.BuildingBlocks.Application.Emails;

public interface IEmailSender
{
    Task SendEmail(EmailMessage message);
}