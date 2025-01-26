using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

internal class UserRegistrationConfirmedHandler(
    IUserRegistrationRepository userRegistrationRepository,
    IUserCreator userCreator)
    : INotificationHandler<UserRegistrationConfirmedDomainEvent>
{
    public async Task Handle(UserRegistrationConfirmedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userRegistration =
            await userRegistrationRepository.GetByIdAsync(notification.UserRegistrationId);

        NotFoundException.ThrowIfNull(userRegistration);

        await userCreator.Create(
            userRegistration.Id,
            userRegistration.Password,
            userRegistration.Email,
            userRegistration.FirstName,
            userRegistration.LastName);
    }
}