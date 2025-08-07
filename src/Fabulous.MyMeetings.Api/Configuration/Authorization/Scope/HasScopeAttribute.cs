using Microsoft.AspNetCore.Authorization;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class HasScopeAttribute(params string[] scopes) : AuthorizeAttribute(HasScopePolicyName)
{
    internal const string HasScopePolicyName = "HasScope";

    public string[] Scopes { get; } = scopes;
}