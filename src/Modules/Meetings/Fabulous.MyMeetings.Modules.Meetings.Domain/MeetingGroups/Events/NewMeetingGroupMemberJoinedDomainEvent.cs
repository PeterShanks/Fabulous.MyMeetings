using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class NewMeetingGroupMemberJoinedDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId, MeetingGroupMemberRole Role): DomainEvent
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public MemberId MemberId { get; } = memberId;
    public MeetingGroupMemberRole Role { get; } = Role;
}