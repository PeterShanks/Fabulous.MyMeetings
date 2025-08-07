using Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ResendUserRegistrationConfirmationEmail;

public class
    ResendUserRegistrationConfirmationEmailCommandHandler(
        IUserRegistrationRepository userRegistrationRepository,
        ICommandsScheduler commandsScheduler) : ICommandHandler<
    ResendUserRegistrationConfirmationEmailCommand>
{
    public async Task Handle(ResendUserRegistrationConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var userRegistration = await userRegistrationRepository.GetByIdEmail(request.Email);

        NotFoundException.ThrowIfNull(userRegistration, $"User with email {request.Email} was not found");

        if (userRegistration.Status == UserRegistrationStatus.Confirmed)
            throw new BusinessException("User email is already confirmed");

        await commandsScheduler.EnqueueAsync(new CreateEmailConfirmationTokenCommand(
            Guid.CreateVersion7(),
            userRegistration.Id));
    }
}