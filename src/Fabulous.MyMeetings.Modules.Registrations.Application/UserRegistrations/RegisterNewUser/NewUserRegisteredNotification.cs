using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

public class NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id) : DomainEventNotification<NewUserRegisteredDomainEvent>(domainEvent, id)
{
}