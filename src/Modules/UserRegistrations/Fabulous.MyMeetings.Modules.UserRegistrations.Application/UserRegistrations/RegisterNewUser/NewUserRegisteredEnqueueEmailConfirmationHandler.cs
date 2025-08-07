using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

internal class NewUserRegisteredEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler) : INotificationHandler<NewUserRegisteredNotification>
{
    public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new CreateEmailConfirmationTokenCommand(
            Guid.CreateVersion7(),
            notification.DomainEvent.UserRegistrationId));
    }
}