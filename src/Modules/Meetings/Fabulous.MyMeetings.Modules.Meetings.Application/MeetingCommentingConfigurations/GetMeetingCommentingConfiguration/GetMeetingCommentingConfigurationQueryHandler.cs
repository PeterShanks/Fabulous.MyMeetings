using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.GetMeetingCommentingConfiguration;

public class GetMeetingCommentingConfigurationQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingCommentingConfigurationQuery, MeetingCommentingConfigurationDto?>
{
    public Task<MeetingCommentingConfigurationDto?> Handle(GetMeetingCommentingConfigurationQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MeetingCommentingConfiguration].[MeetingId],
                [MeetingCommentingConfiguration].[IsCommentingEnabled]
            FROM [Meetings].[MeetingCommentingConfigurations] AS [MeetingCommentingConfiguration]
            WHERE [MeetingCommentingConfiguration].[MeetingId] = @MeetingId
            """;

        return connection.QuerySingleOrDefaultAsync<MeetingCommentingConfigurationDto>(sql,
            new { request.MeetingId });
    }
}