using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetAuthenticatedUserPermissions;

internal class
    GetAuthenticatedUserPermissionsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IUserContext userContext) : IQueryHandler<GetAuthenticatedUserPermissionsQuery,
    List<UserPermissionDto>>
{
    public async Task<List<UserPermissionDto>> Handle(GetAuthenticatedUserPermissionsQuery request,
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
            sql,
            new { UserId = userContext.UserId.Value });

        return permissions.AsList();
    }
}