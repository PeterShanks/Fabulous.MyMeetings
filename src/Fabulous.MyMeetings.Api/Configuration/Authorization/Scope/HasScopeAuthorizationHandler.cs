using Microsoft.AspNetCore.Authorization;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;

public class HasScopeAuthorizationHandler : AttributeAuthorizationHandler
    <HasScopeAuthorizationRequirement, HasScopeAttribute>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasScopeAuthorizationRequirement requirement,
        HasScopeAttribute attribute)
    {
        var user = context.User;

        var scopeClaim = user.Claims.FirstOrDefault(c => c.Type == "scope");
        if (scopeClaim is null)
        {
            context.Fail(new AuthorizationFailureReason(this, $"Missing scope {attribute.Scope}"));
            return Task.CompletedTask;
        }

        var tokenScopes = scopeClaim.Value.Split(' ');

        if (tokenScopes.Contains(attribute.Scope, StringComparer.OrdinalIgnoreCase))
            context.Succeed(requirement);
        else
            context.Fail(new AuthorizationFailureReason(this, $"Missing scope {attribute.Scope}"));

        return Task.CompletedTask;
    }
}