using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.AddAdminUser;

public class AddAdminUserCommand(
    string password,
    string firstName,
    string lastName,
    string name,
    string email) : Command
{
    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string Name { get; } = name;

    public string Email { get; } = email;

    public string Password { get; } = password;
}