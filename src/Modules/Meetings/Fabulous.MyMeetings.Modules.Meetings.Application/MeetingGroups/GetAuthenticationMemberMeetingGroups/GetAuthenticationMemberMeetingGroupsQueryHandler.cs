using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups;

public class GetAuthenticationMemberMeetingGroupsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IMemberContext memberContext): IQueryHandler<GetAuthenticationMemberMeetingGroupsQuery, IEnumerable<MemberMeetingGroupDto>>
{
    public Task<IEnumerable<MemberMeetingGroupDto>> Handle(GetAuthenticationMemberMeetingGroupsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MemberMeetingGroup].[Id],
                [MemberMeetingGroup].[Name],
                [MemberMeetingGroup].[Description],
                [MemberMeetingGroup].[LocationCity],
                [MemberMeetingGroup].[LocationCountryCode],
                [MemberMeetingGroup].[MemberId],
                [MemberMeetingGroup].[RoleCode]
            FROM [Meetings].[v_MemberMeetingGroups] AS [MemberMeetingGroup]
            WHERE [MemberMeetingGroup].[MemberId] = @MemberId
                AND [MemberMeetingGroup].[IsActive] = 1
            """;

        return connection.QueryAsync<MemberMeetingGroupDto>(sql, new
        {
            MemberId = memberContext.MemberId.Value
        });
    }
}