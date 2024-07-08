using Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;

internal class RegisterNewUserCommandHandler : ICommandHandler<RegisterNewUserCommand, Guid>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUsersCounter _usersCounter;

    public RegisterNewUserCommandHandler(IUserRegistrationRepository userRegistrationRepository,
        IUsersCounter usersCounter, IPasswordManager passwordManager)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _usersCounter = usersCounter;
        _passwordManager = passwordManager;
    }

    public Task<Guid> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var password = _passwordManager.HashPassword(request.Password);

        var userRegistration = UserRegistration.RegisterNewUser(
            request.Login,
            password,
            request.Email,
            request.FirstName,
            request.LastName,
            _usersCounter,
            request.ConfirmLink);

        _userRegistrationRepository.Add(userRegistration);

        return Task.FromResult(userRegistration.Id.Value);
    }
}