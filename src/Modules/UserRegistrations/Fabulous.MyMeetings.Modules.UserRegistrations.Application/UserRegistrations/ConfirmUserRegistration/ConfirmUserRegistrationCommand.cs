using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand(Guid userRegistrationId, string token) : Command
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
    public string Token { get; } = token;
}