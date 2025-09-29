using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Logging;

internal static class LoggingModule
{
    public static void AddLogging(this IServiceCollection services, ILoggerFactory loggerFactory)
    {
        services.AddSingleton(loggerFactory);
        services.AddLogging();
    }
}