using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;

public class GetUserPermissionsQuery : Query<List<UserPermissionDto>>
{
    public GetUserPermissionsQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}