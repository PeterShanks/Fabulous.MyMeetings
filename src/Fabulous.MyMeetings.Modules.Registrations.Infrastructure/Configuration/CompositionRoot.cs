using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration;

internal static class CompositionRoot
{
    private static IServiceProvider _serviceProvider = null!;

    internal static void SetContainer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static AsyncServiceScope BeginScope()
    {
        return _serviceProvider.CreateAsyncScope();
    }
}