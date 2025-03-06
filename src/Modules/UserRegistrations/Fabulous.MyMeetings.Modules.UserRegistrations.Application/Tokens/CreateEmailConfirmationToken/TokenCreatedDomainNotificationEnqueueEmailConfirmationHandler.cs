using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken
{
    public class TokenCreatedDomainNotificationEnqueueEmailConfirmationHandler(ICommandsScheduler commandsScheduler) : INotificationHandler<TokenCreatedDomainNotification>
    {
        public Task Handle(TokenCreatedDomainNotification notification, CancellationToken cancellationToken)
        {
            if (notification.DomainEvent.TokenTypeId != TokenTypeId.ConfirmEmail)
                return Task.CompletedTask;

            return commandsScheduler.EnqueueAsync(new SendUserRegistrationConfirmationEmailCommand(
                Guid.NewGuid(),
                notification.DomainEvent.UserRegistrationId,
                notification.DomainEvent.Token));
        }
    }
}
