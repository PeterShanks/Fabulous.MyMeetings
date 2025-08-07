namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;

public class AddMeetingCommentLikeCommand(Guid meetingCommentId): Command
{
    public Guid MeetingCommentId { get; } = meetingCommentId;
}