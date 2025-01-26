using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

public class SendUserRegistrationConfirmationEmailCommand(
    UserRegistrationId userRegistrationId,
    string email) : InternalCommand
{
    internal UserRegistrationId UserRegistrationId { get; } = userRegistrationId;

    internal string Email { get; } = email;
}