using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.Tokens;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.DataAccess;

internal static class DataAccessModule
{
    public static void AddDataAccess(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
            new SqlConnectionFactory(databaseConnectionString));

        services.AddDbContext<UserRegistrationsContext>(b =>
                b.UseSqlServer(databaseConnectionString)
                    .ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>()
#if DEBUG
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
#endif
        );

        services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<DbContext, UserRegistrationsContext>(sp => sp.GetRequiredService<UserRegistrationsContext>());
    }
}