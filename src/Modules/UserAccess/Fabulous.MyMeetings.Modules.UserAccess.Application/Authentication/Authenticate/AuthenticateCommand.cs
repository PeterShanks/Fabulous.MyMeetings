using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

public class AuthenticateCommand(string email, string password) : Command<AuthenticationResult>
{
    public string Email { get; } = email;

    public string Password { get; } = password;
}