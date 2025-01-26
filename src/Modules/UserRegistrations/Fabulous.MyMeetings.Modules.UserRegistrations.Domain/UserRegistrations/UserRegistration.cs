using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Rules;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

public class UserRegistration : Entity, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private UserRegistration()
    {
        // For EF
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private UserRegistration(
        string password,
        string email,
        string firstName,
        string lastName,
        IUsersCounter usersCounter)
    {
        CheckRule(new UserEmailMustBeUniqueRule(usersCounter, email));

        Id = UserRegistrationId.New;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = $"{FirstName} {LastName}";
        RegisterDate = DateTime.UtcNow;
        Status = UserRegistrationStatus.WaitingForConfirmation;

        AddDomainEvent(new NewUserRegisteredDomainEvent(
            Id,
            Email,
            FirstName,
            LastName,
            Name,
            RegisterDate));
    }

    public UserRegistrationId Id { get; }
    public string Password { get; }
    public string Email { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Name { get; }

    public DateTime RegisterDate { get; }

    public UserRegistrationStatus Status { get; private set; }

    public DateTime? ConfirmedDate { get; }

    public static UserRegistration RegisterNewUser(
        string password,
        string email,
        string firstName,
        string lastName,
        IUsersCounter usersCounter)
    {
        return new UserRegistration(password, email, firstName, lastName, usersCounter);
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