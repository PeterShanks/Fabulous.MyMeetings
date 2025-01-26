using System.Text.Json;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.Middlewares
{
    public class ProblemDetailsAuthorizationMiddlewareResultHandler: IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();
        public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult is { Succeeded: false, AuthorizationFailure: not null })
            {
                var scopeValidationErrors = authorizeResult.AuthorizationFailure.FailureReasons
                    .Where(reason => reason.Handler is HasScopeAuthorizationHandler)
                    .ToList();

                if (scopeValidationErrors.Any())
                    return WriteProblemDetailsAsync(context, scopeValidationErrors.First());

                var permissionValidationErrors = authorizeResult.AuthorizationFailure.FailureReasons
                    .Where(reason => reason.Handler is HasPermissionAuthorizationHandler)
                    .ToList();

                if (permissionValidationErrors.Any())
                    return WriteProblemDetailsAsync(context, permissionValidationErrors.First());
            }

            return _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }

        private Task WriteProblemDetailsAsync(HttpContext context, AuthorizationFailureReason reason)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Forbidden",
                Detail = reason.Message,
                Status = StatusCodes.Status403Forbidden,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/problem+json";
            return context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
