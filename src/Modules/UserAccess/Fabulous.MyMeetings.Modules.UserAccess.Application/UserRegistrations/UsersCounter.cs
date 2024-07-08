using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations;

public class UsersCounter : IUsersCounter
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UsersCounter(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public int CountUsersWithLogin(string login)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT COUNT(*)
            FROM [Users].[v_Users] AS U
            WHERE U.Login = @Login
            """;

        return connection.QuerySingle<int>(sql, new { login });
    }
}