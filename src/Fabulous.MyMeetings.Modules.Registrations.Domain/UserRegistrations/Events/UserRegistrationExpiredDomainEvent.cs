using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;

public class UserRegistrationExpiredDomainEvent(UserRegistrationId userRegistrationId) : DomainEvent
{
    public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;
}