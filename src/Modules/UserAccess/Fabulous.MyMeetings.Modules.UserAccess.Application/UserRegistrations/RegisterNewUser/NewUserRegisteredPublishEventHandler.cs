using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    internal class NewUserRegisteredPublishEventHandler: INotificationHandler<NewUserRegisteredNotification>
    {
        private readonly IEventBus _eventBus;

        public NewUserRegisteredPublishEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
        {
            return _eventBus.Publish(new NewUserRegisteredIntegrationEvent(
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
}
