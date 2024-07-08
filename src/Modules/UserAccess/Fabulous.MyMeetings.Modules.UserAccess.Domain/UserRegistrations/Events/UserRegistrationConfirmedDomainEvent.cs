namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;

public class UserRegistrationConfirmedDomainEvent : DomainEvent
{
    public UserRegistrationConfirmedDomainEvent(UserRegistrationId userRegistrationId)
    {
        UserRegistrationId = userRegistrationId;
    }

    public UserRegistrationId UserRegistrationId { get; }
}