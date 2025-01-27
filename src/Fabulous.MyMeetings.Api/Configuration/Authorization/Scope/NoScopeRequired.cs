namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class NoScopeRequired : Attribute
{
}