using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.DataAccess;

public static class DataAccessModule
{
    public static void AddDataAccess(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(_ =>
            new SqlConnectionFactory(databaseConnectionString));

        services.AddDbContext<MeetingsContext>(b =>
                b.UseSqlServer(databaseConnectionString)
                    .ReplaceService<IValueConverterSelector, TypedIdValueConverterSelector>()
#if DEBUG
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
#endif
        );

        services.AddScoped<IMeetingCommentingConfigurationRepository, MeetingCommentingConfigurationRepository>();
        services.AddScoped<IMeetingCommentRepository, MeetingCommentRepository>();
        services.AddScoped<IMeetingGroupProposalRepository, MeetingGroupProposalRepository>();
        services.AddScoped<IMeetingGroupRepository, MeetingGroupRepository>();
        services.AddScoped<IMeetingMemberCommentLikesRepository, MeetingMemberCommentLikesRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IMemberSubscriptionRepository, MemberSubscriptionRepository>();
        services.AddScoped<DbContext, MeetingsContext>(sp => sp.GetRequiredService<MeetingsContext>());
    }
}