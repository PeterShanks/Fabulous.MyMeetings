using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations;
using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;
using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Domain
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
