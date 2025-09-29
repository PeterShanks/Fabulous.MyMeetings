using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Administration.Domain.Members;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.DataAccess;

public static class DataAccessModule
{
    public static void AddDataAccess(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
            new SqlConnectionFactory(databaseConnectionString));

        services.AddDbContext<AdministrationContext>(b =>
                b.UseSqlServer(databaseConnectionString)
                    .ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>()
#if DEBUG
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
#endif
        );

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IMeetingGroupProposalRepository, MeetingGroupProposalRepository>();
        services.AddScoped<DbContext, AdministrationContext>(sp => sp.GetRequiredService<AdministrationContext>());
    }
}