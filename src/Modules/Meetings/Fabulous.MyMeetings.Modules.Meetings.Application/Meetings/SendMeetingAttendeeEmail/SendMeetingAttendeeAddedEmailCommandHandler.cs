using System.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;

public class SendMeetingAttendeeAddedEmailCommandHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IEmailService emailService): ICommandHandler<SendMeetingAttendeeAddedEmailCommand>
{
    public async Task Handle(SendMeetingAttendeeAddedEmailCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        var member = await GetMember(request.AttendeeId, connection);

        var meeting = await GetMeeting(request.MeetingId, connection);

        await emailService.SendEmail(new EmailMessage(
            member.Email,
            member.Name,
            $"You joined to {meeting.Title} meeting.",
            $$"""
              <!DOCTYPE html>
              <html>
              <head>
                <meta charset="UTF-8">
                <title>Meeting Confirmation</title>
              </head>
              <body style="font-family: Arial, sans-serif; background-color: #f4f4f7; padding: 0; margin: 0;">
                <table width="100%" cellspacing="0" cellpadding="0" style="background-color: #f4f4f7; padding: 30px;">
                  <tr>
                    <td align="center">
                      <table width="600" cellspacing="0" cellpadding="0" style="background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.1); padding: 30px;">
                        <tr>
                          <td style="text-align: center;">
                            <h2 style="color: #333333;">🎉 Welcome to the Meeting!</h2>
                            <p style="color: #555555; font-size: 16px;">
                              Hi <strong>{{member.Name}}</strong>,
                            </p>
                            <p style="color: #555555; font-size: 16px;">
                              You have successfully joined the meeting <strong>{{meeting.Title}}</strong>.
                            </p>

                            <table style="margin: 20px 0; background-color: #f0f0f5; border-radius: 6px;" width="100%" cellpadding="12">
                              <tr>
                                <td style="color: #333333;">
                                  <strong>Meeting Date:</strong> {{meeting.TermStartDate.ToShortDateString()}} - {{meeting.TermEndDate.ToShortDateString()}}<br>
                                  <strong>Location:</strong> {{meeting.LocationAddress}}, {{meeting.LocationPostalCode}}, {{meeting.LocationCity}}<br>
                                  <strong>Meeting Description:</strong> {{meeting.Description}}
                                </td>
                              </tr>
                            </table>

                            <p style="color: #555555; font-size: 16px;">
                              If you have any questions or need help, feel free to reply to this email.
                            </p>

                            <a href="{{meeting.Id}}" style="display: inline-block; margin-top: 20px; background-color: #4CAF50; color: white; padding: 12px 20px; text-decoration: none; border-radius: 5px; font-weight: bold;">
                              Rejoin Meeting
                            </a>

                            <p style="color: #999999; font-size: 12px; margin-top: 40px;">
                              Thank you,<br>
                              The Fabulous Meetings Team
                            </p>
                          </td>
                        </tr>
                      </table>

                      <p style="color: #999999; font-size: 12px; margin-top: 20px;">
                        This is an automated message. Please do not reply.
                      </p>
                    </td>
                  </tr>
                </table>
              </body>
              </html>
              """
        ));

    }

    private static async Task<MemberDto> GetMember(MemberId memberId, IDbConnection connection)
    {
        const string sql = $"""
                            SELECT 
                                [Member].Id as [{nameof(MemberDto.Id)}], 
                                [Member].[Name] as [{nameof(MemberDto.Name)}], 
                                [Member].[Login] as [{nameof(MemberDto.Login)}], 
                                [Member].[Email] as [{nameof(MemberDto.Email)}] 
                            FROM [meetings].[v_Members] AS [Member] 
                            WHERE [Member].[Id] = @Id
                            """;
        return await connection.QuerySingleAsync<MemberDto>(
            sql,
            new
            {
                Id = memberId.Value
            });
    }

    private static async Task<MeetingDto> GetMeeting(MeetingId meetingId, IDbConnection connection)
    {
        const string sql = $"""
                            SELECT
                                [Meeting].Id as [{nameof(MeetingDto.Id)}],
                                [Meeting].Title as [{nameof(MeetingDto.Title)}],
                                [Meeting].Description as [{nameof(MeetingDto.Description)}],
                                [Meeting].LocationAddress as [{nameof(MeetingDto.LocationAddress)}],
                                [Meeting].LocationCity as [{nameof(MeetingDto.LocationCity)}],
                                [Meeting].LocationPostalCode as [{nameof(MeetingDto.LocationPostalCode)}],
                                [Meeting].TermStartDate as [{nameof(MeetingDto.TermStartDate)}],
                                [Meeting].TermEndDate as [{nameof(MeetingDto.TermEndDate)}]
                            FROM [meetings].[v_Meetings] AS [Meeting]
                            WHERE [Meeting].[Id] = @Id
                            """;
        return await connection.QuerySingleAsync<MeetingDto>(
            sql,
            new
            {
                Id = meetingId.Value
            });
    }
}