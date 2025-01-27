using System.Reflection;
using System.Text;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Configuration.Authorization
{
    public static class AuthorizationChecker
    {
        public static void CheckAllEndpoints()
        {
            var assembly = typeof(Program).Assembly;
            var allControllerTypes = assembly.GetTypes()
                .Where(x => x.IsSubclassOf(typeof(ControllerBase)));

            var controllerMethods = allControllerTypes
                .SelectMany(controller => controller.GetMethods(), (controller, methodInfo) => new ControllerMethod()
                {
                    Controller = controller,
                    Method = methodInfo
                })
                .Where(m => m.Method.IsPublic && m.Method.DeclaringType == m.Controller)
                .ToList();

            var scopeUnprotectedMethods = GetUnprotectedActionMethods<HasScopeAttribute>(controllerMethods);
            var noScopeUnprotectedMethods = GetUnprotectedActionMethods<NoScopeRequired>(scopeUnprotectedMethods);
            var permissionUnprotectedMethods = GetUnprotectedActionMethods<HasPermissionAttribute>(controllerMethods);
            var methodsWithNoPermissionAttributes = GetUnprotectedActionMethods<NoPermissionRequiredAttribute>(permissionUnprotectedMethods);

            Throw(noScopeUnprotectedMethods.Concat(methodsWithNoPermissionAttributes).ToList());
        }

        private static List<ControllerMethod> GetUnprotectedActionMethods<T>(List<ControllerMethod> controllerMethods)
            where T : Attribute
        {
            var unprotectedMethods = new List<ControllerMethod>();

            foreach (var controllerMethod in controllerMethods)
            {
                var attribute = controllerMethod.Controller.GetCustomAttribute<T>()
                                ?? controllerMethod.Method.GetCustomAttribute<T>();

                if (attribute is null)
                {
                    unprotectedMethods.Add(controllerMethod);
                }
            }

            return unprotectedMethods;
        }

        private static void Throw(List<ControllerMethod> controllerMethods)
        {
            if (!controllerMethods.Any())
            {
                return;
            }

            var errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Invalid authorization configuration: ");

            foreach (var controllerMethod in controllerMethods)
            {
                errorBuilder.AppendLine($"Method {controllerMethod.Controller.Name}.{controllerMethod.Method.Name} is not protected.");
            }

            throw new ApplicationException(errorBuilder.ToString());
        }
    }
}
