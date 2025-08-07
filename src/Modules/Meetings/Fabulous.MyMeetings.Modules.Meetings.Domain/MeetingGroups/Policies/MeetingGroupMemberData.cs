namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies;

public class MeetingGroupMemberData(MeetingGroupId meetingGroupId, MeetingGroupMemberRole role)
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public MeetingGroupMemberRole Role { get; } = role;
}