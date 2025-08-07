using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.DataAccess;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventBus;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Logging;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Mediation;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Quartz;
using Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Authentication;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration;

public class AdministrationStartup
{
    private static IServiceProvider _serviceProvider = null!;
    private static readonly IEnumerable<KeyValuePair<string, object>> ModuleState = [new("Module", "Administration")];

    public static void Initialize(
        string connectionString,
        IExecutionContextAccessor executionContextAccessor,
        ILoggerFactory loggerFactory,
        IHostApplicationLifetime hostApplicationLifetime,
        IEventBus eventBus,
        long? internalProcessingPoolingInterval = null)
    {
        var services = new ServiceCollection();

        var domainNotificationMap = new BiDictionary<string, Type>();
        domainNotificationMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));

        var internalCommandsMap = new BiDictionary<string, Type>();

        services.AddAuthentication();
        services.AddInternalCommandsModule(internalCommandsMap);
        services.AddLogging(loggerFactory);
        services.AddDataAccess(connectionString);
        services.AddMediator();
        services.AddProcessing(domainNotificationMap);
        services.AddEventBus(eventBus);
        services.AddOutbox();
        services.AddQuartz(hostApplicationLifetime, internalProcessingPoolingInterval);
        services.AddSingleton(executionContextAccessor);

        _serviceProvider = services.BuildServiceProvider(new ServiceProviderOptions()
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });
    }

    internal static AsyncServiceScope BeginScope() => _serviceProvider!.CreateAsyncScope();

    internal static IDisposable? BeginLoggerScope()
    {
        return _serviceProvider.GetRequiredService<ILogger<AdministrationStartup>>().BeginScope(ModuleState);
    }
}