using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetAuthenticatedMemberMeetings;

public class GetAuthenticatedMemberMeetingsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IMemberContext memberContext): IQueryHandler<GetAuthenticatedMemberMeetingsQuery, IEnumerable<MemberMeetingDto>>
{
    public Task<IEnumerable<MemberMeetingDto>> Handle(GetAuthenticatedMemberMeetingsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            $"""
             SELECT
                 [Meeting].[Id] AS [{nameof(MemberMeetingDto.MeetingId)}], 
                 [Meeting].[RoleCode] AS [{nameof(MemberMeetingDto.RoleCode)}], 
                 [Meeting].[LocationCity] AS [{nameof(MemberMeetingDto.LocationCity)}], 
                 [Meeting].[LocationAddress] AS [{nameof(MemberMeetingDto.LocationAddress)}], 
                 [Meeting].[LocationPostalCode] AS [{nameof(MemberMeetingDto.LocationPostalCode)}], 
                 [Meeting].[TermStartDate] AS [{nameof(MemberMeetingDto.TermStartDate)}], 
                 [Meeting].[TermEndDate] AS [{nameof(MemberMeetingDto.TermEndDate)}], 
                 [Meeting].[Title] AS [{nameof(MemberMeetingDto.Title)}] 
             FROM [Meetings].[Meetings] AS [Meeting]
             WHERE [Meeting].[AttendeeId] = @AttendeeId AND
                 [Meeting].[IsRemoved] = 0
             """;

        return connection.QueryAsync<MemberMeetingDto>(
            sql,
            new
            {
                AttendeeId = memberContext.MemberId.Value
            });
    }
}