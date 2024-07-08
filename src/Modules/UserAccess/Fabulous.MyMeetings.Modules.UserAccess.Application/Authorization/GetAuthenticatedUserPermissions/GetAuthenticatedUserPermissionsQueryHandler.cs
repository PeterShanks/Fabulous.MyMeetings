using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetAuthenticatedUserPermissions;

internal class
    GetAuthenticatedUserPermissionsQueryHandler : IQueryHandler<GetAuthenticatedUserPermissionsQuery,
    List<UserPermissionDto>>
{
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAuthenticatedUserPermissionsQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IExecutionContextAccessor executionContextAccessor)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<List<UserPermissionDto>> Handle(GetAuthenticatedUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        if (!_executionContextAccessor.IsAvailable) return new List<UserPermissionDto>();

        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                PermissionCode AS Code
            FROM Users.v_UserPermissions
            WHERE UserId = @UserId
            """;

        var permissions = await connection.QueryAsync<UserPermissionDto>(
            sql,
            new { _executionContextAccessor.UserId });

        return permissions.AsList();
    }
}