using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingCanceledDomainEvent(MeetingId meetingId, MemberId cancelMemberId, DateTime cancelDate): DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
    public MemberId CancelMemberId { get; } = cancelMemberId;
    public DateTime CancelDate { get; } = cancelDate;
}