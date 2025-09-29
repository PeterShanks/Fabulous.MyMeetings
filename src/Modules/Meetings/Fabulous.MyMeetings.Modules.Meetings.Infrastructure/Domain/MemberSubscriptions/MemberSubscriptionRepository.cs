using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MemberSubscriptions;

public class MemberSubscriptionRepository(MeetingsContext context): IMemberSubscriptionRepository
{
    public Task<MemberSubscription?> GetByIdOptionalAsync(MemberSubscriptionId memberSubscriptionId)
        => context.MemberSubscriptions
            .FindAsync(memberSubscriptionId)
            .AsTask();

    public Task AddAsync(MemberSubscription memberSubscription)
        => context.MemberSubscriptions.AddAsync(memberSubscription).AsTask();
}