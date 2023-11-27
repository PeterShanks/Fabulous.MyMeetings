using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations.Events;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.RegisterNewUser
{
    public class NewUserRegisteredNotification: DomainEventNotification<NewUserRegisteredDomainEvent>
    {
        public NewUserRegisteredNotification(NewUserRegisteredDomainEvent domainEvent, Guid id) 
            : base(domainEvent, id)
        {
        }
    }
}
