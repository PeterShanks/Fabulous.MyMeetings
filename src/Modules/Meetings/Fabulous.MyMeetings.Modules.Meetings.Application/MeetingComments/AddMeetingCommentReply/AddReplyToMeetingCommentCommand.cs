namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply;

public class AddReplyToMeetingCommentCommand(Guid inReplyToComment, string reply): Command<Guid>
{
    public Guid InReplyToComment { get; } = inReplyToComment;
    public string Reply { get; } = reply;
}