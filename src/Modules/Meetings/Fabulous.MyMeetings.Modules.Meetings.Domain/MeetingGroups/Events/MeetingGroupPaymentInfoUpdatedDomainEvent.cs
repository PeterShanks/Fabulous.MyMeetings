using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

public class MeetingGroupPaymentInfoUpdatedDomainEvent(MeetingGroupId meetingGroupId, DateTime paymentDateTo): DomainEvent
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public DateTime PaymentDateTo { get; } = paymentDateTo;
}