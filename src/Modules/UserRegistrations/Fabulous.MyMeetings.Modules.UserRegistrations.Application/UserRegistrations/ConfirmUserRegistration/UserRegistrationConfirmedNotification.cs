using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration
{
    public class UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent, Guid id) : DomainEventNotification<UserRegistrationConfirmedDomainEvent>(domainEvent, id)
    {
    }
}
