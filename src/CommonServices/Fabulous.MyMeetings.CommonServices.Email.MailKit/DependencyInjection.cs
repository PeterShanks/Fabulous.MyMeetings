using Fabulous.MyMeetings.BuildingBlocks.Application.Emails;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.CommonServices.Email.MailKit;

public static class DependencyInjection
{
    public static IServiceCollection AddMailKit(this IServiceCollection services, Action<MailKitSettings> configuration)
    {
        services.AddSingleton<IEmailService, MailKitEmailService>();
        services.AddOptionsWithValidation<MailKitSettings, MailKitSettingsValidator>(configuration);
        return services;
    }
}