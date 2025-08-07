using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Authentication;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Email;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Logging;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.InternalCommands;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;

public class UserAccessStartup
{
    private static IServiceProvider _serviceProvider = null!;
    private static readonly IEnumerable<KeyValuePair<string, object>> ModuleState = [new("Module", "User Access")];
    public static void Initialize(
        string connectionString,
        IExecutionContextAccessor executionContextAccessor,
        ILoggerFactory loggerFactory,
        IHostApplicationLifetime hostApplicationLifetime,
        IEmailService emailSender,
        IEventBus eventBus,
        long? internalProcessingPoolingInterval = null)
    {
        var services = new ServiceCollection();

        var domainNotificationMap = new BiDictionary<string, Type>();
        var internalCommandMap = new BiDictionary<string, Type>();

        services.AddInternalCommandsModule(internalCommandMap);
        services.AddLogging(loggerFactory);
        services.AddUserAuthentication();
        services.AddDataAccess(connectionString);
        services.AddDomainServices();
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
            throw new InvalidOperationException("UserAccessStartup has not been initialized!");

        using var scope = BeginLoggerScope();
        foreach (var hostedService in _serviceProvider.GetServices<IHostedService>())
        {
            await hostedService.StartAsync(CancellationToken.None);
        }
    }

    public static async Task StopBackgroundServices()
    {
        if (_serviceProvider is null)
            throw new InvalidOperationException("UserAccessStartup has not been initialized!");

        using var scope = BeginLoggerScope();
        foreach (var hostedService in _serviceProvider.GetServices<IHostedService>())
        {
            await hostedService.StopAsync(CancellationToken.None);
        }
    }

    internal static AsyncServiceScope BeginScope() => _serviceProvider.CreateAsyncScope();

    internal static IDisposable? BeginLoggerScope()
    {
        return _serviceProvider.GetRequiredService<ILogger<UserAccessStartup>>().BeginScope(ModuleState);
    }
}