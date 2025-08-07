namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole;

public class SetMeetingAttendeeRoleCommand(Guid memberId, Guid meetingId) : Command
{
    public Guid MemberId { get; } = memberId;

    public Guid MeetingId { get; } = meetingId;
}