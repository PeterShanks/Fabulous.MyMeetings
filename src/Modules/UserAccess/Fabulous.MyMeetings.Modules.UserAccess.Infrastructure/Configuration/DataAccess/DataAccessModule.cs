using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.DataAccess;

internal static class DataAccessModule
{
    public static void AddDataAccess(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
            new SqlConnectionFactory(databaseConnectionString));

         services.AddDbContext<UserAccessContext>(b =>
         {
             b.UseSqlServer(databaseConnectionString)
                 .ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>()
#if DEBUG
                 .EnableDetailedErrors()
                 .EnableSensitiveDataLogging();
#endif
         });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<DbContext, UserAccessContext>(sp => sp.GetRequiredService<UserAccessContext>());
    }
}