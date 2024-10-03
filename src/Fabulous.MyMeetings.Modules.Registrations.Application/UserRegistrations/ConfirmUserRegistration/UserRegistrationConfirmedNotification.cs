using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations.Events;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.ConfirmUserRegistration
{
    public class UserRegistrationConfirmedNotification(UserRegistrationConfirmedDomainEvent domainEvent, Guid id) : DomainEventNotification<UserRegistrationConfirmedDomainEvent>(domainEvent, id)
    {
    }
}
