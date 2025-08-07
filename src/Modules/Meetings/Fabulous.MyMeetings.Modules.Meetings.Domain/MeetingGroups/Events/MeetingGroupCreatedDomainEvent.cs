using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingGroupCreatedDomainEvent(MeetingGroupId meetingGroupId, MemberId creatorId) : DomainEvent
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public MemberId CreatorId { get; } = creatorId;
}