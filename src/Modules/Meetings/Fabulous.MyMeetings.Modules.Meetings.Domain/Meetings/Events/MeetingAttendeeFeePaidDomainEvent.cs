using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingAttendeeFeePaidDomainEvent(MeetingId meetingId, MemberId attendeeId):DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
    public MemberId AttendeeId { get; } = attendeeId;
}