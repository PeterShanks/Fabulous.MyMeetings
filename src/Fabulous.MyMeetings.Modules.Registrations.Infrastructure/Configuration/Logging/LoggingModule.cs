using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Logging;

internal static class LoggingModule
{
    public static void AddLogging(this IServiceCollection services, ILoggerFactory loggerFactory,
        IConfigureOptions<LoggerFilterOptions> logFilterOptions)
    {
        services.AddLogging();

        services.Remove<ILoggerFactory>();
        services.AddSingleton(loggerFactory);

        services.Remove<IConfigureOptions<LoggerFilterOptions>>();
        services.AddSingleton(logFilterOptions);
    }

    private static void Remove<T>(this IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T));
        if (descriptor != null)
            services.Remove(descriptor);
    }
}