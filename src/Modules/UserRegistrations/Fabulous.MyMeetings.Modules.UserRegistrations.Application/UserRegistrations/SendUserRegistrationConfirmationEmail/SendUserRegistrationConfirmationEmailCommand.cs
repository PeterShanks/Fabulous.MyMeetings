using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.SendUserRegistrationConfirmationEmail;

public class SendUserRegistrationConfirmationEmailCommand(
    Guid id,
    Guid userRegistrationId,
    string token) : InternalCommand(id)
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
    public string Token { get; } = token;
}