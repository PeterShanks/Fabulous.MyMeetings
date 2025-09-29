namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee;

public class RemoveMeetingAttendeeCommand(Guid meetingId, Guid attendeeId, string removingReason) : Command
{
    public Guid MeetingId { get; } = meetingId;

    public Guid AttendeeId { get; } = attendeeId;

    public string RemovingReason { get; } = removingReason;
}