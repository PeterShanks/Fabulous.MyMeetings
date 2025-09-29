using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.IntegrationEvents;

public class NewUserRegisteredIntegrationEvent(Guid id, DateTime occurredOn, Guid userId, string email,
    string firstName, string lastName, string name) : IntegrationEvent(id, occurredOn)
{
    public Guid UserId { get; } = userId;

    public string Email { get; } = email;

    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string Name { get; } = name;
}