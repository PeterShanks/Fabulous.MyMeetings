using System.Reflection;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Mediation;

internal static class MediatorModule
{
    public static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Settings.Assemblies.ToArray());
            cfg.Lifetime = ServiceLifetime.Scoped;
            cfg.TypeEvaluator = type =>
            {
                var attributes = type.GetCustomAttribute<SkipAutoRegistrationAttribute>();

                return attributes == null;
            };
            cfg.MediatorImplementationType = typeof(CustomMediator);
        });
    }
}