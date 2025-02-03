using Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Domain;

internal static class DomainModule
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordManager, PasswordManager>();
        // In the future, we might include token service here to support forget password scenarios
    }
}