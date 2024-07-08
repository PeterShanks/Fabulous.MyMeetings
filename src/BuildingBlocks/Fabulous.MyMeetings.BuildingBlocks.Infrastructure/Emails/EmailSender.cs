using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;

public class EmailSender : IEmailSender
{
    private readonly EmailsConfiguration _configuration;
    private readonly ILogger _logger;

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public EmailSender(
        ILogger<EmailSender> logger,
        EmailsConfiguration configuration,
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task SendEmail(EmailMessage message)
    {
        var sqlConnection = _sqlConnectionFactory.GetOpenConnection();

        await sqlConnection.ExecuteScalarAsync(
            """
            INSERT INTO [app].[Emails] ([Id], [From], [To], [Subject], [Content], [Date])
            VALUES (@Id, @From, @To, @Subject, @Content, @Date)
            """,
            new
            {
                Id = Guid.NewGuid(),
                From = _configuration.FromEmail,
                message.To,
                message.Subject,
                message.Content,
                Date = DateTime.UtcNow
            });

        _logger.LogInformation(
            "Email sent. From: {From}, To: {To}, Subject: {Subject}, Content: {Content}",
            _configuration.FromEmail,
            message.To,
            message.Subject,
            message.Content);
    }
}