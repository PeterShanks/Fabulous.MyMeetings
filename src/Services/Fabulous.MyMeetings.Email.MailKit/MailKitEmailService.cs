using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using MailKit.Net.Smtp;
using MimeKit;

namespace Fabulous.MyMeetings.Email.MailKit
{
    internal class MailKitEmailService(MailKitSettings settings, EmailsConfiguration emailsConfiguration): IEmailService
    {
        public async Task SendEmail(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.To.Add(new MailboxAddress(message.ToName, message.ToAddress));
            mimeMessage.From.Add(new MailboxAddress(emailsConfiguration.FromName, emailsConfiguration.FromEmail));
            mimeMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.Content,
            };
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            
            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, true);
            await client.AuthenticateAsync(settings.Username, settings.Password);
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
