namespace Fabulous.MyMeetings.Api.Configuration.Authorization.Permission
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class NoPermissionRequiredAttribute : Attribute
    {
    }
}
