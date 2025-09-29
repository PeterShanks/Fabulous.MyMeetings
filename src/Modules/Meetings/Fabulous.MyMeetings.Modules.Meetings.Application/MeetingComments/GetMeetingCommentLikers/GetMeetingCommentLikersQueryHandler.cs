using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers;

public class GetMeetingCommentLikersQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingCommentLikersQuery, IEnumerable<MeetingCommentLikerDto>>
{
    public Task<IEnumerable<MeetingCommentLikerDto>> Handle(GetMeetingCommentLikersQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [Liker].[Id],
                [Liker].[Name]
            FROM [Meetings].[Members] AS [Liker]
                INNER JOIN [Meetings].[MeetingMemberCommentLikes] AS [Like]
                    ON [Liker].[Id] = [Like].[MemberId]
            WHERE [Like].[MeetingCommentId] = @MeetingCommentId
            """;

        return connection.QueryAsync<MeetingCommentLikerDto>(
            sql,
            new
            {
                request.MeetingCommentId
            });
    }
}