using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;

public class GetMeetingCommentsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingCommentsQuery, IEnumerable<MeetingCommentDto>>
{
    public Task<IEnumerable<MeetingCommentDto>> Handle(GetMeetingCommentsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MeetingComment].[Id],
                [MeetingComment].[InReplyToCommentId],
                [MeetingComment].[AuthorId],
                [MeetingComment].[Comment],
                [MeetingComment].[CreateDate],
                [MeetingComment].[EditDate],
                [MeetingComment].[LikesCount]
            FROM [Meetings].[v_MeetingComments] AS [MeetingComment]
            WHERE [MeetingComment].[MeetingId] = @MeetingId
            """;

        return connection.QueryAsync<MeetingCommentDto>(
            sql,
            new
            {
                request.MeetingId
            });
    }
}