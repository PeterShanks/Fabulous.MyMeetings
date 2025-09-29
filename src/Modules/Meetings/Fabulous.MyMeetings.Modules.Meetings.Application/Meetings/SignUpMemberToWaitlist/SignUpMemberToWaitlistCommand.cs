namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SignUpMemberToWaitlist;

public class SignUpMemberToWaitlistCommand(Guid meetingId) : Command
{
    public Guid MeetingId { get; } = meetingId;
}