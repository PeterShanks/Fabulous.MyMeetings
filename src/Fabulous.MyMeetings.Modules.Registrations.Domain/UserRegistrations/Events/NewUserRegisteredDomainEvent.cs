using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;

public class NewUserRegisteredDomainEvent(
    UserRegistrationId userRegistrationId,
    string login,
    string email,
    string firstName,
    string lastName,
    string name,
    DateTime registerDate,
    string confirmLink) : DomainEvent
{
    public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;

    public string Login { get; } = login;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string Name { get; } = name;

    public DateTime RegisterDate { get; } = registerDate;

    public string ConfirmLink { get; } = confirmLink;
}