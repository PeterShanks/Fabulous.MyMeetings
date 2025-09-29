using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.DataAccess;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Emails;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.EventBus;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Mediation;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Outbox;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Quartz;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;
using Fabulous.MyMeetings.Modules.Meetings.Application.Members.CreateMember;
using Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Authentication;
using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.InternalCommands;
using Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;

public class MeetingsStartup
{
    private static IServiceProvider _serviceProvider = null!;
    private static readonly IEnumerable<KeyValuePair<string, object>> ModuleState = [new("Module", "Registrations")];

    public static void Initialize(
        string connectionString,
        IExecutionContextAccessor executionContextAccessor,
        ILoggerFactory loggerFactory,
        IHostApplicationLifetime hostApplicationLifetime,
        SiteSettings siteSettings,
        IEmailService emailSender,
        IEventBus eventBus,
        long? internalProcessingPoolingInterval = null)
    {
        var services = new ServiceCollection();

        var domainNotificationMap = new BiDictionary<string, Type>();
        domainNotificationMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
        domainNotificationMap.Add("MeetingGroupProposedNotification", typeof(MeetingGroupProposedNotification));
        domainNotificationMap.Add("MeetingGroupCreatedNotification", typeof(MeetingGroupCreatedNotification));
        domainNotificationMap.Add("MeetingAttendeeAddedNotification", typeof(MeetingAttendeeAddedNotification));
        domainNotificationMap.Add("MemberCreatedNotification", typeof(MemberCreatedNotification));
        domainNotificationMap.Add("MemberSubscriptionExpirationDateChangedNotification", typeof(MemberSubscriptionExpirationDateChangedNotification));
        domainNotificationMap.Add("MeetingCommentLikedNotification", typeof(MeetingCommentLikedNotification));
        domainNotificationMap.Add("MeetingCommentUnlikedNotification", typeof(MeetingCommentUnlikedNotification));

        var internalCommandsMap = new BiDictionary<string, Type>();
        internalCommandsMap.Add("CreateMemberCommand", typeof(CreateMemberCommand));
        internalCommandsMap.Add("ChangeSubscriptionExpirationDateForMemberCommand", typeof(ChangeSubscriptionExpirationDateForMemberCommand));
        internalCommandsMap.Add("MarkMeetingAttendeeFeeAsPayedCommand", typeof(MarkMeetingAttendeeFeeAsPayedCommand));
        internalCommandsMap.Add("SendMeetingAttendeeAddedEmailCommand", typeof(SendMeetingAttendeeAddedEmailCommand));
        internalCommandsMap.Add("SetMeetingGroupExpirationDateCommand", typeof(SetMeetingGroupExpirationDateCommand));
        internalCommandsMap.Add("SendMeetingGroupCreatedEmailCommand", typeof(SendMeetingGroupCreatedEmailCommand));
        internalCommandsMap.Add("CreateNewMeetingGroupCommand", typeof(CreateNewMeetingGroupCommand));
        internalCommandsMap.Add("AcceptMeetingGroupProposalCommand", typeof(AcceptMeetingGroupProposalCommand));

        services.AddLogging(loggerFactory);
        services.AddInternalCommandsModule(internalCommandsMap);
        services.AddAuthentication();
        services.AddDataAccess(connectionString);
        services.AddMediator();
        services.AddProcessing(domainNotificationMap);
        services.AddEventBus(eventBus);
        services.AddOutbox();
        services.AddQuartz(hostApplicationLifetime, internalProcessingPoolingInterval);
        services.AddEmail(emailSender);
        services.AddSingleton(executionContextAccessor);

        _serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions()
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });
    }

    public static async Task StartBackgroundServices()
    {
        if (_serviceProvider is null)
            throw new InvalidOperationException("RegistrationsStartup has not been initialized!");

        using var scope = BeginLoggerScope();
        foreach (var hostedService in _serviceProvider.GetServices<IHostedService>())
        {
            await hostedService.StartAsync(CancellationToken.None);
        }
    }

    public static async Task StopBackgroundServices()
    {
        if (_serviceProvider is null)
            throw new InvalidOperationException("RegistrationsStartup has not been initialized!");

        using var scope = BeginLoggerScope();
        foreach (var hostedService in _serviceProvider.GetServices<IHostedService>())
        {
            await hostedService.StopAsync(CancellationToken.None);
        }
    }

    internal static AsyncServiceScope BeginScope() => _serviceProvider!.CreateAsyncScope();

    internal static IDisposable? BeginLoggerScope()
    {
        return _serviceProvider.GetRequiredService<ILogger<MeetingsStartup>>().BeginScope(ModuleState);
    }
}