namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;

public class RemoveMeetingCommentLikeCommand(Guid meetingCommentId): Command
{
    public Guid MeetingCommentId { get; } = meetingCommentId;
}