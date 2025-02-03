using FluentValidation;

namespace Fabulous.MyMeetings.BuildingBlocks.Application
{
    public class SiteSettingsValidator: AbstractValidator<SiteSettings>
    {
        public SiteSettingsValidator()
        {
            RuleFor(x => x.SiteUrl)
                .NotEmpty();
        }
    }
}
