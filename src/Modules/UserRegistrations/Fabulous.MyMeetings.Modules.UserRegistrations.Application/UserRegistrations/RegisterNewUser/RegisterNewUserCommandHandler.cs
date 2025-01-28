using Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

internal class RegisterNewUserCommandHandler(IUserRegistrationRepository userRegistrationRepository,
    IUsersCounter usersCounter, IPasswordManager passwordManager) : ICommandHandler<RegisterNewUserCommand, Guid>
{
    public Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var password = passwordManager.HashPassword(request.Password);

        var userRegistration = UserRegistration.RegisterNewUser(
            password,
            request.Email,
            request.FirstName,
            request.LastName,
            usersCounter);

        userRegistrationRepository.Add(userRegistration);

        return Task.FromResult(userRegistration.Id.Value);
    }
}