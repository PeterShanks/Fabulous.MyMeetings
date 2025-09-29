using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingEditedDomainEvent(Guid meetingId) : DomainEvent
{
    public Guid MeetingId { get; } = meetingId;
}