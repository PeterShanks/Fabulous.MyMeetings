using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;

public class GetMeetingGroupDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingGroupDetailsQuery, MeetingGroupDetailsDto>
{
    public Task<MeetingGroupDetailsDto> Handle(GetMeetingGroupDetailsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT 
                [MeetingGroup].[Id],
                [MeetingGroup].[Name],
                [MeetingGroup].[Description],
                [MeetingGroup].[LocationCity],
                [MeetingGroup].[LocationCountryCode],
                (
                    SELECT COUNT(*)
                    FROM [Meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember]
                    WHERE [MeetingGroupMember].[MeetingGroupId] = [MeetingGroup].[Id]
                ) AS [MembersCount]
            FROM [Meetings].[v_MeetingGroups] AS [MeetingGroup]
            WHERE [MeetingGroup].[Id] = @MeetingGroupId
            """;

        return connection.QuerySingleAsync<MeetingGroupDetailsDto>(
            sql, new
            {
                request.MeetingGroupId
            });
    }
}