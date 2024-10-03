using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

public class AuthenticateCommand(string login, string password) : Command<AuthenticationResult>
{
    public string Login { get; } = login;

    public string Password { get; } = password;
}