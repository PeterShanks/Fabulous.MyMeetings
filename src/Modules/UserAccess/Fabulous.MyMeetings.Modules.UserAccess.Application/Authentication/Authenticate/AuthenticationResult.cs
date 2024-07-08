namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

public class AuthenticationResult
{
    public AuthenticationResult(string authenticationError)
    {
        IsAuthenticated = false;
        AuthenticationError = authenticationError;
    }

    public AuthenticationResult(UserDto user)
    {
        IsAuthenticated = true;
        User = user;
    }

    public bool IsAuthenticated { get; }

    public string? AuthenticationError { get; }

    public UserDto? User { get; }

    public static AuthenticationResult Success(UserDto user)
    {
        return new AuthenticationResult(user);
    }

    public static AuthenticationResult Failure(string error)
    {
        return new AuthenticationResult(error);
    }
}