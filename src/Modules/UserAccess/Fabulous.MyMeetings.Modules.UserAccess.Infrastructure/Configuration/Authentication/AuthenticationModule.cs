using Fabulous.MyMeetings.Modules.UserAccess.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Authentication;

public static class AuthenticationModule
{
    public static IServiceCollection AddUserAuthentication(this IServiceCollection services)
    {
        return services.AddScoped<IUserContext, UserContext>();
    }
}