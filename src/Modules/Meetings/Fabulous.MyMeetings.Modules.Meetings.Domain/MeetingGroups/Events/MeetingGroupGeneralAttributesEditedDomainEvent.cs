using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingGroupGeneralAttributesEditedDomainEvent(MeetingGroupId meetingGroupId, string newName, string description, MeetingGroupLocation newLocation): DomainEvent
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public string NewName { get; } = newName;
    public string Description { get; } = description;
    public MeetingGroupLocation NewLocation { get; } = newLocation;
}