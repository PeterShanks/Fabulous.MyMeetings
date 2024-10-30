using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.UserAccess
{
    public static class UserAccessDependencyModule
    {
        public static void AddUserAccess(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessModule, UserAccessModule>();
        }
    }
}
