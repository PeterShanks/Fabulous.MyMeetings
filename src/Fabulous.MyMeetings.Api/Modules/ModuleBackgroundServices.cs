using Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;

namespace Fabulous.MyMeetings.Api.Modules
{
    public class ModuleBackgroundServices : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(
                UserAccessStartup.StartBackgroundServices(),
                RegistrationsStartup.StartBackgroundServices()
            );
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.WhenAll(
                UserAccessStartup.StopBackgroundServices(),
                RegistrationsStartup.StopBackgroundServices()
            );
        }
    }
}
