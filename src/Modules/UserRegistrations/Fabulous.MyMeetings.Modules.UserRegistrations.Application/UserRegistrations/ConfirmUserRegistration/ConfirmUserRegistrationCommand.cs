using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand(Guid userRegistrationId) : Command
{
    public Guid UserRegistrationId { get; } = userRegistrationId;
}