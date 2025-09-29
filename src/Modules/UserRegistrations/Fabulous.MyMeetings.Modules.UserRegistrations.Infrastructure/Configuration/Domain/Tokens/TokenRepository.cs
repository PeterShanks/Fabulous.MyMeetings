using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.Tokens;

internal class TokenRepository(UserRegistrationsContext dbContext, TimeProvider timeProvider) : ITokenRepository
{
    public Task AddAsync(Token token)
    {
        return dbContext.Tokens.AddAsync(token).AsTask();
    }

    public ValueTask<Token?> GetAsync(Guid tokenId)
    {
        return dbContext.Tokens.FindAsync(tokenId);
    }

    public Task<List<Token>> GetUnusedTokensAsync(Guid userId, TokenTypeId tokenTypeId)
    {
        return dbContext.Tokens
            .Where(t => 
                t.UserId == userId && 
                t.TokenTypeId == tokenTypeId 
                && t.ExpiresAt > timeProvider.GetUtcNow().UtcDateTime &&
                !t.IsUsed &&
                !t.IsInvalidated)
            .ToListAsync();
    }
}