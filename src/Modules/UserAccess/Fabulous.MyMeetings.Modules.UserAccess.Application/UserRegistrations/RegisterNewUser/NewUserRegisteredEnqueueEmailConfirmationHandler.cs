using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;

internal class NewUserRegisteredEnqueueEmailConfirmationHandler : INotificationHandler<NewUserRegisteredNotification>
{
    private readonly ICommandsScheduler _commandsScheduler;

    public NewUserRegisteredEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler)
    {
        _commandsScheduler = commandsScheduler;
    }

    public Task Handle(NewUserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        return _commandsScheduler.EnqueueAsync(new SendUserRegistrationConfirmationEmailCommand(
            notification.DomainEvent.UserRegistrationId,
            notification.DomainEvent.Email,
            notification.DomainEvent.ConfirmLink));
    }
}