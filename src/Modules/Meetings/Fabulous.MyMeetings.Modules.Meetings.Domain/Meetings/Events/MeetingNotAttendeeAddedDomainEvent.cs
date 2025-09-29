using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingNotAttendeeAddedDomainEvent(MeetingId meetingId, MemberId memberId): DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
    public MemberId MemberId { get; } = memberId;
}