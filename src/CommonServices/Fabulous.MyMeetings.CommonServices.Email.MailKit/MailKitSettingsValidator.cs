using FluentValidation;

namespace Fabulous.MyMeetings.CommonServices.Email.MailKit;

public class MailKitSettingsValidator : AbstractValidator<MailKitSettings>
{
    public MailKitSettingsValidator()
    {
        RuleFor(p => p.Host)
            .NotEmpty();

        RuleFor(p => p.Port)
            .GreaterThan(0);

        RuleFor(p => p.Username)
            .NotEmpty();

        RuleFor(p => p.Password)
            .NotEmpty();
    }
}