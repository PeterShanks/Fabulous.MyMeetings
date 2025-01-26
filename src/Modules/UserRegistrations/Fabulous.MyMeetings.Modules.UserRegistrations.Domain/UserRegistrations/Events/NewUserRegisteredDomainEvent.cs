using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;

public class NewUserRegisteredDomainEvent(
    UserRegistrationId userRegistrationId,
    string email,
    string firstName,
    string lastName,
    string name,
    DateTime registerDate) : DomainEvent
{
    public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string Name { get; } = name;

    public DateTime RegisterDate { get; } = registerDate;
}