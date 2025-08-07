namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingCommentLikers;

public class GetMeetingCommentLikersQuery(Guid meetingCommentId): Query<IEnumerable<MeetingCommentLikerDto>>
{
    public Guid MeetingCommentId { get; } = meetingCommentId;
}