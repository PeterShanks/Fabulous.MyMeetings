using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;

public class EmailService(
    ILogger<EmailService> logger,
    EmailsConfiguration configuration,
    ISqlConnectionFactory sqlConnectionFactory,
    TimeProvider timeProvider) : IEmailService
{
    private readonly ILogger _logger = logger;

    public async Task SendEmail(EmailMessage message)
    {
        var sqlConnection = sqlConnectionFactory.GetOpenConnection();

        await sqlConnection.ExecuteScalarAsync(
            """
            INSERT INTO [app].[Emails] ([Id], [From], [To], [Subject], [Content], [Date])
            VALUES (@Id, @From, @To, @Subject, @Content, @Date)
            """,
            new
            {
                Id = Guid.CreateVersion7(),
                From = configuration.FromEmail,
                message.ToAddress,
                message.Subject,
                message.Content,
                Date = timeProvider.GetUtcNow().UtcDateTime
            });

        _logger.LogInformation(
            "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}",
            configuration.FromEmail,
            message.ToAddress,
            message.Subject,
            message.Content);
    }
}