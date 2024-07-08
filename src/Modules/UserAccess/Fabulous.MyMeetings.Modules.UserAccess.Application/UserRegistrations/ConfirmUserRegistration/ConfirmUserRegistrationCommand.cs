using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

public class ConfirmUserRegistrationCommand : Command
{
    public ConfirmUserRegistrationCommand(Guid userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public Guid UserRegistrationId { get; }
}