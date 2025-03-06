using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken
{
    public class CreateEmailConfirmationTokenCommand(
        Guid id,
        UserRegistrationId userRegistrationId
        ): InternalCommand(id)
    {
        public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;
    }
}
