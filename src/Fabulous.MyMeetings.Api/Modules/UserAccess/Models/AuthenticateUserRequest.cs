using Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication.Authenticate;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess.Models
{
    public class AuthenticateUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

        public AuthenticateCommand ToCommand()
        {
            return new AuthenticateCommand(Email, Password);
        }
    }
}
