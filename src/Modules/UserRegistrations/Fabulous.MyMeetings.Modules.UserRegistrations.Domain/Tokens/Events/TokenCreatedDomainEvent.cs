using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Events;

public class TokenCreatedDomainEvent(
    Guid tokenId,
    Guid userRegistrationId,
    TokenTypeId tokenTypeId,
    string token): DomainEvent
{
    public Guid TokenId { get; } = tokenId;
    public Guid UserRegistrationId { get; } = userRegistrationId;
    public TokenTypeId TokenTypeId { get; } = tokenTypeId;
    public string Token { get; } = token;
}