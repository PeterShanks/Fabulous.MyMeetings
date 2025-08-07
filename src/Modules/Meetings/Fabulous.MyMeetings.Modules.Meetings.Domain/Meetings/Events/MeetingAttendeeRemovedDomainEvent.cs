using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingAttendeeRemovedDomainEvent(MemberId memberId, MeetingId meetingId, string reason): DomainEvent
{
    public MemberId MemberId { get; } = memberId;
    public MeetingId MeetingId { get; } = meetingId;
    public string Reason { get; } = reason;
}