using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

internal class ConfirmUserRegistrationCommandHandler(IUserRegistrationRepository userRegistrationRepository) : ICommandHandler<ConfirmUserRegistrationCommand>
{
    public async Task Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration =
            await userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

        userRegistration!.Confirm();
    }
}