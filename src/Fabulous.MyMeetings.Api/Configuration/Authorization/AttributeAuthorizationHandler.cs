using Microsoft.AspNetCore.Authorization;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute>
        : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : AuthorizeAttribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
                throw new InvalidOperationException("Couldn't retrieve HttpContext");
            
            var endpoint = httpContext.GetEndpoint() as RouteEndpoint
                ?? throw new InvalidOperationException("Couldn't find endpoint");

            var attribute = endpoint?.Metadata.GetMetadata<TAttribute>() 
                            ?? throw new InvalidOperationException($"Couldn't retrieve attribute {typeof(TAttribute).Name} on {endpoint!.DisplayName}");

            return HandleRequirementAsync(context, requirement, attribute);
        }

        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            TRequirement requirement, 
            TAttribute attribute);
    }
}
