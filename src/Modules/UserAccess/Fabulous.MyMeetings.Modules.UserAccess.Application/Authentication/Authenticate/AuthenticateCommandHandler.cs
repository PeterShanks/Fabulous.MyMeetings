using System.Security.Claims;
using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, AuthenticationResult>
{
    private readonly IPasswordManager _passwordManager;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory, IPasswordManager passwordManager)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _passwordManager = passwordManager;
    }

    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
                Login,
                Name,
                Email,
                IsActive,
                Password
            FROM Users.v_Users
            WHERE Id = @Login
            """;

        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(
            sql, new { request.Login });

        if (user == null)
            return AuthenticationResult.Failure("Incorrect login or password");

        if (!user.IsActive)
            return AuthenticationResult.Failure("User is not active");

        var verifyPasswordResult = _passwordManager.VerifyHashedPassword(user.Password, request.Password);

        if (verifyPasswordResult == PasswordVerificationResult.Failed)
            return new AuthenticationResult("Incorrect login or password");

        user.Claims = new List<Claim>
        {
            new(CustomClaimTypes.Name, user.Name),
            new(CustomClaimTypes.Email, user.Email)
        };

        return AuthenticationResult.Success(user);
    }
}