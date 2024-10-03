using Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

public class SendUserRegistrationConfirmationEmailCommand(
    UserRegistrationId userRegistrationId,
    string email,
    string confirmLink) : InternalCommand
{
    internal UserRegistrationId UserRegistrationId { get; } = userRegistrationId;

    internal string Email { get; } = email;

    internal string ConfirmLink { get; } = confirmLink;
}