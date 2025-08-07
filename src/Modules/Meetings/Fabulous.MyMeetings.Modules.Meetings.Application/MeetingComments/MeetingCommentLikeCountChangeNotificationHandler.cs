using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments;

public class MeetingCommentLikeCountChangeNotificationHandler(ISqlConnectionFactory sqlConnectionFactory) 
    : INotificationHandler<MeetingCommentLikedNotification>, INotificationHandler<MeetingCommentUnlikedNotification>
{
    public Task Handle(MeetingCommentLikedNotification notification, CancellationToken cancellationToken)
    {
        return CountLikes(notification.DomainEvent.MeetingCommentId);
    }

    public Task Handle(MeetingCommentUnlikedNotification notification, CancellationToken cancellationToken)
    {
        return CountLikes(notification.DomainEvent.MeetingCommentId);
    }

    private Task CountLikes(Guid meetingCommentId)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            UPDATE [Meetings].[MeetingComments]
                SET [LikesCount] = 
                    (
                        SELECT COUNT(*)
                        FROM [Meetings].[MeetingMemberCommentLikes]
                        WHERE [MeetingCommentId] = @MeetingCommentId
                    )
            WHERE [Id] = @MeetingCommentId
            """;

        return connection.ExecuteAsync(sql, new
        {
            MeetingCommentId = meetingCommentId
        });
    }
}