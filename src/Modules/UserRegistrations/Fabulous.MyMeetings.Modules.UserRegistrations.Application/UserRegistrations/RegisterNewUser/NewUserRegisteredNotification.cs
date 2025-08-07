using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations.Events;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

public class NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id) : DomainEventNotification<NewUserRegisteredDomainEvent>(domainEvent, id);