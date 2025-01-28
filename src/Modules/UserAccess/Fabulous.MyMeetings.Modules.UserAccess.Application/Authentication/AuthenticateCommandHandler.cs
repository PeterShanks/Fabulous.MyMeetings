using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

internal class AuthenticateCommandHandler(ISqlConnectionFactory sqlConnectionFactory, IPasswordManager passwordManager) : ICommandHandler<AuthenticateCommand, AuthenticationResult>
{
    public async Task<AuthenticationResult> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                *
            FROM Users.v_Users
            """;

        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(
            sql, new { request.Email });

        if (user == null)
            return AuthenticationResult.Failure("Incorrect email or password");

        if (!user.IsActive)
            return AuthenticationResult.Failure("User is not active");

        var verifyPasswordResult = passwordManager.VerifyHashedPassword(user.Password, request.Password);

        if (verifyPasswordResult == PasswordVerificationResult.Failed)
            return AuthenticationResult.Failure("Incorrect email or password");

        return AuthenticationResult.Success(user.Id);
    }
}