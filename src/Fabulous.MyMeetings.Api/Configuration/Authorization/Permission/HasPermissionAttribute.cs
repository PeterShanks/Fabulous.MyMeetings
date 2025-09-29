using Microsoft.AspNetCore.Authorization;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;

public class HasPermissionAttribute(string permissionName) : AuthorizeAttribute(HasPermissionPolicyName)
{
    internal const string HasPermissionPolicyName = "HasPermission";

    public string PermissionName { get; } = permissionName;
}