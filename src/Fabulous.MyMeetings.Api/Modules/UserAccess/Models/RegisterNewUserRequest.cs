using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess.Models;

public class RegisterNewUserRequest
{
    public required string Password { get; set; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }


    public RegisterNewUserCommand ToCommand()
        => new RegisterNewUserCommand(
            Password,
            Email,
            FirstName,
            LastName);
}