using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Rules;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; }
        public string Login { get; }
        public string Password { get; }
        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Name { get; }

        public DateTime RegisterDate { get; }

        public UserRegistrationStatus Status { get; private set; }

        public DateTime? ConfirmedDate { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private UserRegistration()
        {
            // For EF
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static UserRegistration RegisterNewUser(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            return new UserRegistration(login, password, email, firstName, lastName, usersCounter, confirmLink);
        }

        private UserRegistration(
            string login,
            string password,
            string email,
            string firstName,
            string lastName,
            IUsersCounter usersCounter,
            string confirmLink)
        {
            CheckRule(new UserLoginMustBeUniqueRule(usersCounter, login));

            Id = UserRegistrationId.New;
            Login = login;
            Password = password;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Name = $"{FirstName} {LastName}";
            RegisterDate = DateTime.UtcNow;
            Status = UserRegistrationStatus.WaitingForConfirmation;

            AddDomainEvent(new NewUserRegisteredDomainEvent(
                Id,
                Login,
                Email,
                FirstName,
                LastName,
                Name,
                RegisterDate,
                confirmLink));
        }

        public User CreateUser()
        {
            CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(Status));

            return User.CreateFromUserRegistration(Id, Login, Password, 
                Email, FirstName, LastName, Name);
        }

        public void Confirm()
        {
            CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(Status));
            CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(Status));

            Status = UserRegistrationStatus.Confirmed;

            AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Id));
        }

        public void Expire()
        {
            CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(Status));

            Status = UserRegistrationStatus.Expired;

            AddDomainEvent(new UserRegistrationExpiredDomainEvent(Id));
        }
    }
}
