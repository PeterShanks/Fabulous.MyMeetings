using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations;

public class UsersCounter(ISqlConnectionFactory sqlConnectionFactory) : IUsersCounter
{
    public int CountUsersWithEmail(string email)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT COUNT(*)
            FROM [UserRegistrations].[v_UserRegistrations] AS U
            WHERE U.Email = @email
            """;

        return connection.QuerySingle<int>(sql, new { email });
    }
}