using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Email;

internal static class EmailModule
{
    public static void AddEmail(this IServiceCollection services, IEmailService emailSender)
    {
        services.AddSingleton<IEmailService>(_ => emailSender);
    }
}