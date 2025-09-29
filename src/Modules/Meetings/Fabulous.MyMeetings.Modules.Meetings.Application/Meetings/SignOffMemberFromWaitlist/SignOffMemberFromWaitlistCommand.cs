namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SignOffMemberFromWaitlist;

public class SignOffMemberFromWaitlistCommand(Guid meetingId):Command
{
    public Guid MeetingId { get; } = meetingId;
}