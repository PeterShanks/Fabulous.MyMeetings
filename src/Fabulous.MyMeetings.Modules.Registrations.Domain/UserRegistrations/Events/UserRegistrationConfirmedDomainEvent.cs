using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;

public class UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId) : DomainEvent
{
    public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;
}