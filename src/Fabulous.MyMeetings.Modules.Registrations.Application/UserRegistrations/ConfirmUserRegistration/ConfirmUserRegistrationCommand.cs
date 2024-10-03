using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand(Guid userRegistrationId) : Command
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
}