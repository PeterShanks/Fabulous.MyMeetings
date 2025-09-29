using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Events;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens.Rules;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;

public class Token : Entity
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public string Value { get; } = null!;
    public TokenTypeId TokenTypeId { get; }
    public DateTime CreatedAt { get; }
    public DateTime ExpiresAt { get; }
    public bool IsUsed { get; private set; }
    public DateTime? UsedAt { get; private set; }
    public bool IsInvalidated { get; private set; }
    public DateTime? InvalidatedAt { get; private set; }

    private Token()
    {

    }

    internal Token(Guid id, Guid userId, TokenTypeId tokenTypeId, string value, DateTime createdAt, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        TokenTypeId = tokenTypeId;
        Value = value;
        CreatedAt = createdAt;
        ExpiresAt = expiresAt;
        IsUsed = false;
        UsedAt = null;
        IsInvalidated = false;
        InvalidatedAt = null;

        AddDomainEvent(new TokenCreatedDomainEvent(id, userId, tokenTypeId, value));
    }

    internal void SetAsUsed()
    {
        CheckRule(new TokenCannotBeUsedMoreThanOnceRule(IsUsed));
        CheckRule(new TokenCannotBeUsedWhenExpiredRule(ExpiresAt));
        CheckRule(new TokenCannotBeUsedWhenInvalidatedRule(IsInvalidated));

        IsUsed = true;
        UsedAt = DateTime.UtcNow;

        AddDomainEvent(new TokenUsedDomainEvent(Id, UserId, TokenTypeId));
    }

    internal void Invalidate()
    {
        CheckRule(new TokenCannotBeInvalidatedMoreThanOnceRule(IsInvalidated));
        CheckRule(new TokenCannotBeInvalidatedWhenExpiredRule(ExpiresAt));
        CheckRule(new TokenCannotBeInvalidatedWhenItIsUsedRule(IsUsed));

        IsInvalidated = true;
        InvalidatedAt = DateTime.UtcNow;

        AddDomainEvent(new TokenInvalidatedDomainEvent(Id, UserId, TokenTypeId));
    }
}