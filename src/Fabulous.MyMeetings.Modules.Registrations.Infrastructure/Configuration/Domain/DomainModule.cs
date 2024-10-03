using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Domain
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
