using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

internal class RegisterNewUserCommandHandler(IUserRegistrationRepository userRegistrationRepository,
    IUsersCounter usersCounter, IPasswordManager passwordManager) : ICommandHandler<RegisterNewUserCommand, Guid>
{
    private readonly IPasswordManager _passwordManager = passwordManager;

    public Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var password = _passwordManager.HashPassword(request.Password);

        var userRegistration = UserRegistration.RegisterNewUser(
            request.Login,
            password,
            request.Email,
            request.FirstName,
            request.LastName,
            usersCounter,
            request.ConfirmLink);

        userRegistrationRepository.Add(userRegistration);

        return Task.FromResult(userRegistration.Id.Value);
    }
}