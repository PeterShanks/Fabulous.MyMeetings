using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;

public class UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId) : DomainEvent
{
    public UserRegistrationId UserRegistrationId { get; } = userRegistrationId;
}