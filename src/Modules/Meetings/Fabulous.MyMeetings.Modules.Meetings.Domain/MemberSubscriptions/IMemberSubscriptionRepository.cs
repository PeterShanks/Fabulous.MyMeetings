namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;

public interface IMemberSubscriptionRepository
{
    Task<MemberSubscription?> GetByIdOptionalAsync(MemberSubscriptionId memberSubscriptionId);

    Task AddAsync(MemberSubscription memberSubscription);
}