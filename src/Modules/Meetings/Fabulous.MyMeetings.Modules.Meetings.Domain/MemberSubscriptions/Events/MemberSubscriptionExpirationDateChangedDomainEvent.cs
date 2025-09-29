using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions.Events;

public class MemberSubscriptionExpirationDateChangedDomainEvent(MemberId memberId, DateTime expirationDate)
    : DomainEvent
{
    public MemberId MemberId { get; } = memberId;
    public DateTime ExpirationDate { get; } = expirationDate;
}