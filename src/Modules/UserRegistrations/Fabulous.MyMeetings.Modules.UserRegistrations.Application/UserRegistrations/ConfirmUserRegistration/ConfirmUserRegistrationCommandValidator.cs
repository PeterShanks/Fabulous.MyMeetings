using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommandValidator : AbstractValidator<ConfirmUserRegistrationCommand>
{
    public ConfirmUserRegistrationCommandValidator()
    {
        RuleFor(x => x.UserRegistrationId).NotEmpty();
        RuleFor(x => x.Token).NotEmpty();
    }
}