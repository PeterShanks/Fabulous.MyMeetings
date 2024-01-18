using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.DataAccess;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Email;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.EventBus;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Logging;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Mediation;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Quartz;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration
{
    public static class UserAccessStartup
    {
        public static Task Initialize(
            IConfiguration configuration,
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILoggerFactory loggerFactory,
            IConfigureOptions<LoggerFilterOptions> loggerFilterOptions,
            EmailsConfiguration emailsConfiguration,
            IEmailSender? emailSender,
            IEventBus? eventBus,
            long? internalProcessingPoolingInterval = null)
        {

            var host = new HostBuilder()
                .ConfigureHostConfiguration(c => c.AddConfiguration(configuration))
                .ConfigureAppConfiguration(cfg => cfg.AddConfiguration(configuration))
                .ConfigureServices(services =>
                {
                    var domainNotificationMap = new BiDictionary<string, Type>();
                    domainNotificationMap.Add("NewUserRegisteredNotification", typeof(NewUserRegisteredNotification));

                    services.AddLogging(loggerFactory, loggerFilterOptions);
                    services.AddDataAccess(connectionString);
                    services.AddDomainServices();
                    services.AddMediator();
                    services.AddProcessing(domainNotificationMap);
                    services.AddEventBus(eventBus);
                    services.AddOutbox();
                    services.AddQuartz(connectionString, internalProcessingPoolingInterval);
                    services.AddEmail(emailsConfiguration, emailSender);
                    services.AddSingleton(executionContextAccessor);
                })
                .Build();

            CompositionRoot.SetContainer(host.Services);

            return host.StartAsync();
        }
    }
}
