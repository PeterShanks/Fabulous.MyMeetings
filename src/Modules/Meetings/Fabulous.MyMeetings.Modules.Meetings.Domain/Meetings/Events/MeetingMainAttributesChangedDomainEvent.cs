using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingMainAttributesChangedDomainEvent(MeetingId meetingId): DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
}