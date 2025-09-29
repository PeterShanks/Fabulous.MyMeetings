using Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions;

public class SubscriptionExpirationDateChangedIntegrationEventHandler(ICommandsScheduler commandsScheduler)
    : INotificationHandler<SubscriptionExpirationDateChangedIntegrationEvent>
{
    public Task Handle(SubscriptionExpirationDateChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new ChangeSubscriptionExpirationDateForMemberCommand(
            Guid.CreateVersion7(),
            new MemberId(notification.PayerId),
            notification.ExpirationDate));
    }
}