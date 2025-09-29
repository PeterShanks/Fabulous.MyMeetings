namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingNotAttendee;

public class AddMeetingNotAttendeeCommand(Guid meetingId): Command
{
    public Guid MeetingId { get; } = meetingId;
}