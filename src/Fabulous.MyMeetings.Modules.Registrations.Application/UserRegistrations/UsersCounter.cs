using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations;

public class UsersCounter(ISqlConnectionFactory sqlConnectionFactory) : IUsersCounter
{
    public int CountUsersWithLogin(string login)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT COUNT(*)
            FROM [Registrations].[v_Users] AS U
            WHERE U.Login = @Login
            """;

        return connection.QuerySingle<int>(sql, new { login });
    }
}