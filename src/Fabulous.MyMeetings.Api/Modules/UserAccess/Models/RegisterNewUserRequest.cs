using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess.Models
{
    public class RegisterNewUserRequest
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public RegisterNewUserCommand ToCommand()
            => new RegisterNewUserCommand(
                Password,
                Email,
                FirstName,
                LastName);
    }
}
