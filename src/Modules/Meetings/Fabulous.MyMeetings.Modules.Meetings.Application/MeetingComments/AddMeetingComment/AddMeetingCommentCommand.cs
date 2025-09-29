namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;

public class AddMeetingCommentCommand(Guid meetingId, string comment): Command<Guid>
{
    public Guid MeetingId { get; } = meetingId;
    public string Comment { get; } = comment;
}