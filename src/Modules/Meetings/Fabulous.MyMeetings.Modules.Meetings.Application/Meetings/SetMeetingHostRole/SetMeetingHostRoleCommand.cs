namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole;

public class SetMeetingHostRoleCommand(Guid memberId, Guid meetingId): Command
{
    public Guid MemberId { get; } = memberId;
    public Guid MeetingId { get; } = meetingId;
}