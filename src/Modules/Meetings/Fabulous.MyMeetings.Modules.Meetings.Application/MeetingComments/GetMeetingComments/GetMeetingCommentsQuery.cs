namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments;

public class GetMeetingCommentsQuery(Guid meetingId):Query<IEnumerable<MeetingCommentDto>>
{
    public Guid MeetingId { get; } = meetingId;
}