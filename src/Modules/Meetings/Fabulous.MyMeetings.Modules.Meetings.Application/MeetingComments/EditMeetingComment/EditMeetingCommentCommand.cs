namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;

public class EditMeetingCommentCommand(Guid meetingCommentId, string editedComment): Command
{
    public Guid MeetingCommentId { get; } = meetingCommentId;
    public string EditedComment { get; } = editedComment;
}