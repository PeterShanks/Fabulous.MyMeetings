using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users.Events;

namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private readonly List<UserRole> _roles;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User()
    {
        // For EF
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private User(
        Guid id,
        string password,
        string email,
        string firstName,
        string lastName,
        string name,
        UserRole role)
    {
        Id = new UserId(id);
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;

        IsActive = true;

        _roles =
        [
            role
        ];

        AddDomainEvent(new UserCreatedDomainEvent(Id));
    }

    public UserId Id { get; }
    public string Password { get; }

    public string Email { get; }

    public bool IsActive { get; }

    public string FirstName { get; }

    public string LastName { get; }

    public string Name { get; }
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    public static User CreateAdmin(
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        return new User(
            Guid.NewGuid(),
            password,
            email,
            firstName,
            lastName,
            name,
            UserRole.Administrator);
    }

    public static User CreateUser(
        Guid userId,
        string password,
        string email,
        string firstName,
        string lastName)
    {
        return new User(
            userId,
            password,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            UserRole.Member);
    }
}