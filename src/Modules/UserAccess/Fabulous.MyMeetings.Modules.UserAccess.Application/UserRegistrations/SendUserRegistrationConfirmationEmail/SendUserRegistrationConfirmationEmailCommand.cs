using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.SendUserRegistrationConfirmationEmail
{
    public class SendUserRegistrationConfirmationEmailCommand: InternalCommand
    {
        public SendUserRegistrationConfirmationEmailCommand(
            UserRegistrationId userRegistrationId,
            string email,
            string confirmLink)
        {
            UserRegistrationId = userRegistrationId;
            Email = email;
            ConfirmLink = confirmLink;
        }

        internal UserRegistrationId UserRegistrationId { get; }

        internal string Email { get; }

        internal string ConfirmLink { get; }
    }
}
