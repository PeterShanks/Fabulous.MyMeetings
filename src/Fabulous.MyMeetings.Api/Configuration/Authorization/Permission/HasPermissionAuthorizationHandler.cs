using Microsoft.AspNetCore.Authorization;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Permission
{
    public class HasPermissionAuthorizationHandler()
        : AttributeAuthorizationHandler<HasPermissionAuthorizationRequirement, HasPermissionAttribute>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasPermissionAuthorizationRequirement requirement,
            HasPermissionAttribute attribute)
        {
            var user = context.User;

            var permissionsClaim = user.Claims.FirstOrDefault(c => c.Type == "permissions");

            if (permissionsClaim is null)
            {
                context.Fail(new AuthorizationFailureReason(this, $"User does not have permission {attribute.PermissionName}"));
                return Task.CompletedTask;
            }

            var tokenPermissions = permissionsClaim.Value.Split(' ');

            if (!tokenPermissions.Contains(attribute.PermissionName))
                context.Fail(new AuthorizationFailureReason(this, $"User does not have permission {attribute.PermissionName}"));
            else
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
