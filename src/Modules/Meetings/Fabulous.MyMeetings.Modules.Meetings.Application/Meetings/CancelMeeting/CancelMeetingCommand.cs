namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CancelMeeting;

public class CancelMeetingCommand(Guid meetingId): Command
{
    public Guid MeetingId { get; } = meetingId;
}