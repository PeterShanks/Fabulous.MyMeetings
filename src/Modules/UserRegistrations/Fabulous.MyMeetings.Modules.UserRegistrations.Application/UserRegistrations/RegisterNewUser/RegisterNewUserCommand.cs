using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

public class RegisterNewUserCommand(
    string password,
    string email,
    string firstName,
    string lastName) : Command<Guid>
{
    public string Password { get; } = password;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;
}