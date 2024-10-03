using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;

public class GetUserPermissionsQuery(Guid userId) : Query<List<UserPermissionDto>>
{
    public Guid UserId { get; } = userId;
}