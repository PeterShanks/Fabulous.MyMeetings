using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Email;

internal static class EmailModule
{
    public static void AddEmail(this IServiceCollection services, EmailsConfiguration configuration,
        IEmailSender? emailSender)
    {
        if (emailSender is not null)
        {
            services.AddSingleton<IEmailSender>(_ => emailSender);
        }
        else
        {
            services.AddSingleton(configuration);
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}