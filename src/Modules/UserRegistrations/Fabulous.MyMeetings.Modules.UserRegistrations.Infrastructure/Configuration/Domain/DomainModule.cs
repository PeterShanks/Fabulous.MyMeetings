using Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain
{
    internal static class DomainModule
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersCounter, UsersCounter>();
            services.AddScoped<IPasswordManager, PasswordManager>();
            services.AddScoped<IUserCreator, UserAccessGateway>();
        }
    }
}
