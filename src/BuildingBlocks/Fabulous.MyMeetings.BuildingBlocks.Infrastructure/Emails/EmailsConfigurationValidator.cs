using FluentValidation;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.Emails
{
    public class EmailsConfigurationValidator: AbstractValidator<EmailsConfiguration>
    {
        public EmailsConfigurationValidator()
        {
            RuleFor(p => p.FromEmail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.FromName)
                .NotEmpty();
        }
    }
}
