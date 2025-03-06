using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Events;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Tokens.CreateEmailConfirmationToken
{
    public class TokenCreatedDomainNotification(TokenCreatedDomainEvent domainEvent, Guid id) : DomainEventNotification<TokenCreatedDomainEvent>(domainEvent, id)
    {
    }
}
