using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ResendUserRegistrationConfirmationEmail
{
    public class ResendUserRegistrationConfirmationEmailCommand(string email) : Command
    {
        public string Email { get; } = email;
    }
}
