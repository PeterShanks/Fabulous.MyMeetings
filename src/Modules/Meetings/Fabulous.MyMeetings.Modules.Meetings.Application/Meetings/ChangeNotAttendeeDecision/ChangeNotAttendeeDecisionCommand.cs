namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.ChangeNotAttendeeDecision;

public class ChangeNotAttendeeDecisionCommand(Guid meetingId): Command
{
    public Guid MeetingId { get; } = meetingId;
}