using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

public class RegisterNewUserCommand(
    string login,
    string password,
    string email,
    string firstName,
    string lastName,
    string confirmLink) : Command<Guid>
{
    public string Login { get; } = login;

    public string Password { get; } = password;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string ConfirmLink { get; } = confirmLink;
}