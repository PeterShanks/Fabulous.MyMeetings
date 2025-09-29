namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;

public class RemoveMeetingCommentCommand(Guid meetingCommentId, string reason) : Command
{
    public Guid MeetingCommentId { get; } = meetingCommentId;
    public string Reason { get; } = reason;
}