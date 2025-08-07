using Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration;
using Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration;

namespace Fabulous.MyMeetings.Api.Configuration;

public class ModuleBackgroundServices : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(
            UserAccessStartup.StartBackgroundServices(),
            UserRegistrationsStartup.StartBackgroundServices(),
            MeetingsStartup.StartBackgroundServices()
        );
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(
            UserAccessStartup.StopBackgroundServices(),
            UserRegistrationsStartup.StopBackgroundServices(),
            MeetingsStartup.StopBackgroundServices()
        );
    }
}