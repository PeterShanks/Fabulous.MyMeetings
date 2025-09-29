using Fabulous.MyMeetings.Modules.Administration.Domain.Users;
using Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Authentication;

internal static class AuthenticationModule
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        return services.AddScoped<IUserContext, UserContext>();
    }
}