using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees;

public class GetMeetingAttendeesQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingAttendeesQuery, IEnumerable<MeetingAttendeeDto>>
{
    public Task<IEnumerable<MeetingAttendeeDto>> Handle(GetMeetingAttendeesQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            $"""
             SELECT
                 [MeetingAttendee].[FirstName] AS [{nameof(MeetingAttendeeDto.FirstName)}],
                 [MeetingAttendee].[LastName] AS [{nameof(MeetingAttendeeDto.LastName)}],
                 [MeetingAttendee].[RoleCode] AS [{nameof(MeetingAttendeeDto.RoleCode)}],
                 [MeetingAttendee].[DecisionDate] AS [{nameof(MeetingAttendeeDto.DecisionDate)}],
                 [MeetingAttendee].[GuestsNumber] AS [{nameof(MeetingAttendeeDto.GuestsNumber)}],
                 [MeetingAttendee].[AttendeeId] AS [{nameof(MeetingAttendeeDto.AttendeeId)}]
             FROM [meetings].[v_MeetingAttendees] AS [MeetingAttendee]
             WHERE [MeetingAttendee].[MeetingId] = @MeetingId
             """;

        return connection.QueryAsync<MeetingAttendeeDto>(
            sql,
            new
            {
                MeetingId = request.MeetingId
            });
    }
}