using FluentValidation;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

internal class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Login cannot be empty");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
    }
}