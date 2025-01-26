using System.Reflection;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization;

public struct ControllerMethod
{
    public Type Controller { get; set; }
    public MethodInfo Method { get; set; }
}