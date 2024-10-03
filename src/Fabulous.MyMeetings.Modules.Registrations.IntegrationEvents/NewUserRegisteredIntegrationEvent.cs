using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Registrations.IntegrationEvents;

public class NewUserRegisteredIntegrationEvent(Guid id, DateTime occurredOn, Guid userId, string login, string email,
    string firstName, string lastName, string name) : IntegrationEvent(id, occurredOn)
{
    public Guid UserId { get; } = userId;

    public string Login { get; } = login;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string Name { get; } = name;
}