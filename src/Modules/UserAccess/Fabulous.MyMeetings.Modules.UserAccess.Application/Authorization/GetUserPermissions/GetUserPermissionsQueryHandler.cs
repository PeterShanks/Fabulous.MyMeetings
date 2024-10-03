using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;

internal class GetUserPermissionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetUserPermissionsQuery, List<UserPermissionDto>>
{
    public async Task<List<UserPermissionDto>> Handle(GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                PermissionCode AS Code
            FROM Users.v_UserPermissions
            WHERE UserId = @UserId
            """;

        var permissions = await connection.QueryAsync<UserPermissionDto>(
            sql, new { request.UserId });

        return permissions.AsList();
    }
}