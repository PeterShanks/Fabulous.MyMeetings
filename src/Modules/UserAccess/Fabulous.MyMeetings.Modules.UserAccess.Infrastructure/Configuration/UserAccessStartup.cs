using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Email;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Logging;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
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
        EmailsConfiguration emailsConfiguration,
        IHostApplicationLifetime hostApplicationLifetime,
        IEmailSender? emailSender = null,
        IEventBus? eventBus = null,
        long? internalProcessingPoolingInterval = null)
    {
        var services = new ServiceCollection();

        var domainNotificationMap = new BiDictionary<string, Type>();

        services.AddLogging(loggerFactory);
        services.AddDataAccess(connectionString);
        services.AddDomainServices();
        services.AddMediator();
        services.AddProcessing(domainNotificationMap);
        services.AddEventBus(eventBus);
        services.AddOutbox();
        //services.AddQuartz(hostApplicationLifetime, internalProcessingPoolingInterval);
        services.AddEmail(emailsConfiguration, emailSender);
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
            await hostedService.StartAsync(default);
        }
    }

    public static async Task StopBackgroundServices()
    {
        if (_serviceProvider is null)
            throw new InvalidOperationException("UserAccessStartup has not been initialized!");

        using var scope = BeginLoggerScope();
        foreach (var hostedService in _serviceProvider.GetServices<IHostedService>())
        {
            await hostedService.StopAsync(default);
        }
    }

    internal static AsyncServiceScope BeginScope() => _serviceProvider!.CreateAsyncScope();

    internal static IDisposable? BeginLoggerScope()
    {
        return _serviceProvider.GetRequiredService<ILogger<UserAccessStartup>>().BeginScope(ModuleState);
    }
}