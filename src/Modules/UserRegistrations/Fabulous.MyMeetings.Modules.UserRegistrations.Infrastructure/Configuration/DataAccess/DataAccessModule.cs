using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.DataAccess
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
            services.AddScoped<DbContext, RegistrationsContext>();
        }
    }
}
