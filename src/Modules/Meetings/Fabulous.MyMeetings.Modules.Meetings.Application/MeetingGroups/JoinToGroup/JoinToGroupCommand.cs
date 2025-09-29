namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup;

public class JoinToGroupCommand(Guid meetingGroupId) : Command
{
    public Guid MeetingGroupId { get; } = meetingGroupId;
}