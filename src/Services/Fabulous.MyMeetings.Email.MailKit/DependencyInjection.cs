using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Email.MailKit
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailService, MailKitEmailService>();
            services.AddOptionsWithValidation<MailKitSettings, MailKitSettingsValidator>(configuration);
            return services;
        }
    }
}
