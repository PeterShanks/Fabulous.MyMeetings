using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken;

public class CreateEmailConfirmationTokenCommandHandler(ITokenService tokenService) : ICommandHandler<CreateEmailConfirmationTokenCommand>
{
    public Task Handle(CreateEmailConfirmationTokenCommand request, CancellationToken cancellationToken)
    {
        return tokenService.CreateAsync(request.UserRegistrationId, TokenTypeId.ConfirmEmail);
    }
}