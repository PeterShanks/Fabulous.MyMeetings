using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;

public class GetAllMeetingGroupsQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetAllMeetingGroupsQuery, IEnumerable<MeetingGroupDto>>
{
    public Task<IEnumerable<MeetingGroupDto>> Handle(GetAllMeetingGroupsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MeetingGroup].[Id],
                [MeetingGroup].[Name],
                [MeetingGroup].[Description],
                [MeetingGroup].[LocationCity],
                [MeetingGroup].[LocationCountryCode]
            FROM [Meetings].[v_MeetingGroups] AS [MeetingGroup]
            """;

        return connection.QueryAsync<MeetingGroupDto>(sql);
    }
}