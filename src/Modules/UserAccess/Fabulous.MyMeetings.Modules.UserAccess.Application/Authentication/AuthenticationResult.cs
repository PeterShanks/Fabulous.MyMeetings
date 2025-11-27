namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

public class AuthenticationResult
{
    private AuthenticationResult(string authenticationError)
    {
        IsAuthenticated = false;
        AuthenticationError = authenticationError;
    }

    private AuthenticationResult(Guid userId)
    {
        IsAuthenticated = true;
        UserId = userId;
    }

    public bool IsAuthenticated { get; }

    public string? AuthenticationError { get; }

    public Guid? UserId { get; }

    public static AuthenticationResult Success(Guid userId) => new(userId);

    public static AuthenticationResult Failure(string error)
    {
        return new AuthenticationResult(error);
    }
}