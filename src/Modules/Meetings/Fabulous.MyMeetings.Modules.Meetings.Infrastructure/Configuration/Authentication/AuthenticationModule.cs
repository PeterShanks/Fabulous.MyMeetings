using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Authentication;

internal static class AuthenticationModule
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        return services.AddScoped<IMemberContext, MemberContext>();
    }
}