using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Mediation;

internal static class MediatorModule
{
    public static void AddMediator(this IServiceCollection services)
    {
        foreach (var assembly in Assemblies)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}