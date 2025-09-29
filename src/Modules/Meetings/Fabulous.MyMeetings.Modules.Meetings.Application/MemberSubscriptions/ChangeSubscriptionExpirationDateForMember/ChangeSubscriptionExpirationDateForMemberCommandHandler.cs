using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember;

public class ChangeSubscriptionExpirationDateForMemberCommandHandler(
    IMemberSubscriptionRepository memberSubscriptionRepository): ICommandHandler<ChangeSubscriptionExpirationDateForMemberCommand>
{
    public async Task Handle(ChangeSubscriptionExpirationDateForMemberCommand request, CancellationToken cancellationToken)
    {
        var memberSubscription =
            await memberSubscriptionRepository.GetByIdOptionalAsync(new MemberSubscriptionId(request.MemberId));

        if (memberSubscription is null)
        {
            memberSubscription = MemberSubscription.CreateForMember(request.MemberId, request.ExpirationDate);
            await memberSubscriptionRepository.AddAsync(memberSubscription);
        }
        else
        {
            memberSubscription.ChangeExpirationDate(request.ExpirationDate);
        }
    }
}