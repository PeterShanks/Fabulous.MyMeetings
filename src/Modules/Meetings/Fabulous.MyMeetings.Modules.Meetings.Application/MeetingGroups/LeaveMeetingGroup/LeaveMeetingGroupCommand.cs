namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.LeaveMeetingGroup;

public class LeaveMeetingGroupCommand(Guid meetingGroupId) : Command
{
    public Guid MeetingGroupId { get; } = meetingGroupId;
}