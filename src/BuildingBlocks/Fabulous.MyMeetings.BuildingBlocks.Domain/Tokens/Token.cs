namespace Fabulous.MyMeetings.BuildingBlocks.Domain.Tokens
{
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
        }

        internal void SetAsUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }
    }
}
