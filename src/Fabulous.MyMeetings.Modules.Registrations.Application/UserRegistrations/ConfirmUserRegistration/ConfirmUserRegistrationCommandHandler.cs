using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;

internal class ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository) : ICommandHandler<ConfirmUserRegistrationCommand>
{
    public async Task Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration =
            await userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

        userRegistration!.Confirm();
    }
}