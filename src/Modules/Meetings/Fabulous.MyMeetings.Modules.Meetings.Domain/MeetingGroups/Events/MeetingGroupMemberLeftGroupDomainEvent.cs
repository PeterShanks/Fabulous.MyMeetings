using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

internal class MeetingGroupMemberLeftGroupDomainEvent(MeetingGroupId meetingGroupId, MemberId memberId):DomainEvent
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public MemberId MemberId { get; } = memberId;
}