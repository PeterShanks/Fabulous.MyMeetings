namespace Fabulous.MyMeetings.BuildingBlocks.Application.Emails;

public interface IEmailService
{
    Task SendEmail(EmailMessage message);
}