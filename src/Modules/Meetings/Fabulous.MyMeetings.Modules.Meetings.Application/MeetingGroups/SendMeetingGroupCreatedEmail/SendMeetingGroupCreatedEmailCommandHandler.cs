using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail;

public class SendMeetingGroupCreatedEmailCommandHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IEmailService emailService): ICommandHandler<SendMeetingGroupCreatedEmailCommand>
{
    public async Task Handle(SendMeetingGroupCreatedEmailCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MeetingGroup].[Name] AS GroupName,
                [MeetingGroup].[Description],
                [MeetingGroup].[LocationCity],
                [MeetingGroup].[LocationCountryCode],
                [Member].[FirstName],
                [Member].[Email]
            FROM [Meetings].[v_MeetingGroups] AS [MeetingGroup]
                INNER JOIN [Meetings].[Members] AS [Member]
                    ON [MeetingGroup].[CreatorId] = [Member].[Id]
            WHERE [MeetingGroup].[Id] = @MeetingGroupId
                AND [Member].[Id] = @CreatorId
            """;

        var emailContentData = await connection.QuerySingleAsync<EmailDataDto>(sql, new
        {
            MeetingGroupId = request.MeetingGroupId.Value,
            CreatorId = request.CreatorId.Value
        });

        var emailHtmlTemplate =
            $$"""
              <!DOCTYPE html>
              <html>
              <head>
                  <meta charset="UTF-8">
                  <meta name="viewport" content="width=device-width, initial-scale=1.0">
                  <title>Meeting Group Created</title>
                  <style>
                      body {
                          font-family: Arial, sans-serif;
                          background-color: #f4f4f4;
                          margin: 0;
                          padding: 0;
                      }
                      .container {
                          max-width: 600px;
                          margin: 20px auto;
                          background: #ffffff;
                          padding: 20px;
                          border-radius: 8px;
                          box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                          text-align: center;
                      }
                      .header {
                          background: #007bff;
                          color: #ffffff;
                          padding: 15px;
                          font-size: 20px;
                          font-weight: bold;
                          border-radius: 8px 8px 0 0;
                      }
                      .content {
                          padding: 20px;
                          font-size: 16px;
                          color: #333333;
                      }
                      .footer {
                          margin-top: 20px;
                          font-size: 12px;
                          color: #777777;
                      }
                  </style>
              </head>
              <body>
                  <div class="container">
                      <div class="header">Meeting Group Created 🎉</div>
                      <div class="content">
                          <p>Dear <strong>{{emailContentData.FirstName}}</strong>,</p>
                          <p>Your meeting group, <strong>{{emailContentData.GroupName}}</strong>, has been successfully created in <strong>Fabulous.MyMeetings</strong>! 🚀</p>
                          <p>📍 Location: <strong>{{emailContentData.LocationCity}}, {{emailContentData.LocationCountryCode}}</strong></p>
                          <p>This group will help you organize and conduct productive meetings effortlessly.</p>
                          <p><strong>Next Steps:</strong></p>
                          <ul style="text-align: left; display: inline-block;">
                              <li>📩 Invite members to join your group.</li>
                              <li>📅 Schedule meetings and send reminders.</li>
                              <li>📝 Share agendas and notes with your team.</li>
                          </ul>
                      </div>
                      <div class="footer">
                          <p>If you have any questions, feel free to contact our support team.</p>
                          <p>&copy; 2025 Fabulous.MyMeetings. All rights reserved.</p>
                      </div>
                  </div>
              </body>
              </html>
              """;

        await emailService.SendEmail(new EmailMessage(
            emailContentData.Email,
            emailContentData.FirstName,
            "Meeting Group Created",
            emailHtmlTemplate));

    }

    private class EmailDataDto
    {
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string LocationCity { get; set; }
        public string LocationCountryCode { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
    }
}