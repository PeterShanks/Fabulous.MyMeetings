using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.DataAccess
{
    internal static class DataAccessModule
    {
        public static void AddDataAccess(this IServiceCollection services, string databaseConnectionString)
        {
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
                new SqlConnectionFactory(databaseConnectionString));

            services.AddDbContext<RegistrationsContext>(b =>
                b.UseSqlServer(databaseConnectionString));

            services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
        }
    }
}
