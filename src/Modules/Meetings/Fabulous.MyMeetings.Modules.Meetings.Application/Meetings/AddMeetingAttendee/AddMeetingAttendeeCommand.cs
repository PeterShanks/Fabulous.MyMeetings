namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingAttendee;

public class AddMeetingAttendeeCommand(Guid meetingId, int guestsNumber): Command
{
    public Guid MeetingId { get; } = meetingId;
    public int GuestsNumber { get; } = guestsNumber;
}