using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users.Events;

namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.Users
{
    public class User: Entity, IAggregateRoot
    {
        public UserId Id { get; }
        public string Login { get; }

        public string Password { get; }

        public string Email { get; }

        public bool IsActive { get; set; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        private List<UserRole> _roles;
        public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private User()
        {
            // For EF
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static User CreateAdmin(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string name)
        {
            return new User(
                Guid.NewGuid(),
                login,
                password,
                email,
                firstName,
                lastName,
                name,
                UserRole.Administrator);
        }

        internal static User CreateFromUserRegistration(
            UserRegistrationId userRegistrationId,
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string name)
        {
            return new User(
                userRegistrationId.Value,
                login,
                password,
                email,
                firstName,
                lastName,
                name,
                UserRole.Member);
        }

        private User(
            Guid id,
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            string name,
            UserRole role)
        {
            Id = new UserId(id);
            Login = login;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = name;

            IsActive = true;

            _roles = new List<UserRole>()
            {
                role
            };

            AddDomainEvent(new UserCreatedDomainEvent(Id));
        }
    }
}
