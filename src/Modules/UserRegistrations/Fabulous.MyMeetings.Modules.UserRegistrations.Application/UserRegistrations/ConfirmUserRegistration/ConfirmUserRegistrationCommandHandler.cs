using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

internal class ConfirmUserRegistrationCommandHandler(
    IUserRegistrationRepository userRegistrationRepository,
    ITokenService tokenService) : ICommandHandler<ConfirmUserRegistrationCommand>
{
    public async Task Handle(ConfirmUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        var userRegistration =
            await userRegistrationRepository.GetByIdAsync(new UserRegistrationId(request.UserRegistrationId));

        NotFoundException.ThrowIfNull(userRegistration);

        await tokenService.ValidateAsync(
            request.Token, 
            TokenTypeId.ConfirmEmail, 
            request.UserRegistrationId);

        userRegistration!.Confirm();
    }
}