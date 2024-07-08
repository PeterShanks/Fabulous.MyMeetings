using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

public class AuthenticateCommand : Command<AuthenticationResult>
{
    public AuthenticateCommand(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public string Login { get; }

    public string Password { get; }
}