using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;
using Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain
{
    internal static class DomainModule
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersCounter, UsersCounter>();
            services.AddScoped<IPasswordManager, PasswordManager>();
        }
    }
}
