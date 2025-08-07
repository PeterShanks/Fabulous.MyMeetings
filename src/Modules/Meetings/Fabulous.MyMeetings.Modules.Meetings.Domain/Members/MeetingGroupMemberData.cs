using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

public class MeetingGroupMemberData(MeetingGroupId meetingGroupId, MemberId memberId)
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;

    public MemberId MemberId { get; } = memberId;
}