using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Registrations.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

internal class NewUserRegisteredPublishEventHandler(IEventBus eventBus) : INotificationHandler<NewUserRegisteredNotification>
{
    public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return eventBus.Publish(new NewUserRegisteredIntegrationEvent(
            notification.DomainEvent.Id,
            notification.DomainEvent.OccurredOn,
            notification.DomainEvent.UserRegistrationId.Value,
            notification.DomainEvent.Login,
            notification.DomainEvent.Email,
            notification.DomainEvent.FirstName,
            notification.DomainEvent.LastName,
            notification.DomainEvent.Name
        ));
    }
}