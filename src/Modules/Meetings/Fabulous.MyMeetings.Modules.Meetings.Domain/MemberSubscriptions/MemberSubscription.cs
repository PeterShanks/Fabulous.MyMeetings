using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;

public class MemberSubscription: Entity, IAggregateRoot
{
    public MemberSubscriptionId Id { get; }
    public DateTime ExpirationDate { get; private set; }

    private MemberSubscription()
    {
        // For EF
    }

    private MemberSubscription(MemberId memberId, DateTime expirationDate)
    {
        Id = new MemberSubscriptionId(memberId);
        ExpirationDate = expirationDate;

        AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(memberId, expirationDate));
    }

    public static MemberSubscription CreateForMember(MemberId memberId, DateTime expirationDate)
        => new MemberSubscription(memberId, expirationDate);

    public void ChangeExpirationDate(DateTime expirationDate)
    {
        ExpirationDate = expirationDate;

        AddDomainEvent(new MemberSubscriptionExpirationDateChangedDomainEvent(new MemberId(Id), expirationDate));
    }
}