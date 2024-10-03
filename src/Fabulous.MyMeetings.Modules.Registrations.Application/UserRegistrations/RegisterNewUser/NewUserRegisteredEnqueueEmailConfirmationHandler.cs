using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

internal class NewUserRegisteredEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler) : INotificationHandler<NewUserRegisteredNotification>
{
    public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new SendUserRegistrationConfirmationEmailCommand(
            notification.DomainEvent.UserRegistrationId,
            notification.DomainEvent.Email,
            notification.DomainEvent.ConfirmLink));
    }
}