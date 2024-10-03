using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.CreateUser
{
    public class CreateUserCommand(
        Guid userId,
        string login,
        string email,
        string firstName,
        string lastName,
        string password)
        : Command
    {
        public Guid UserId { get; } = userId;

        public string Login { get; } = login;

        public string Email { get; } = email;

        public string FirstName { get; } = firstName;

        public string LastName { get; } = lastName;

        public string Password { get; } = password;
    }
}
