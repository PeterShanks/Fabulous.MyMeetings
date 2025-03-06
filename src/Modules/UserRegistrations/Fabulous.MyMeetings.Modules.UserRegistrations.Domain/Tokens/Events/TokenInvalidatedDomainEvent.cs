using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Events;

public class TokenInvalidatedDomainEvent(
    Guid tokenId,
    Guid userId,
    TokenTypeId tokenTypeId) : DomainEvent
{
    public Guid TokenId { get; } = tokenId;
    public Guid UserId { get; } = userId;
    public TokenTypeId TokenTypeId { get; } = tokenTypeId;
}