using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions;

public class MemberSubscriptionExpirationDateChangedNotification(MemberSubscriptionExpirationDateChangedDomainEvent domainEvent, Guid id) 
    : DomainEventNotification<MemberSubscriptionExpirationDateChangedDomainEvent>(domainEvent, id);