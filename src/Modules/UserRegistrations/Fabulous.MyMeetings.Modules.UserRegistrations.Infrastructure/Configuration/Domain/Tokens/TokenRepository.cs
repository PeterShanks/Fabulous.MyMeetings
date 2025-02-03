using Fabulous.MyMeetings.BuildingBlocks.Domain.Tokens;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.Tokens
{
    internal class TokenRepository(UserRegistrationsContext dbContext) : ITokenRepository
    {
        public Task AddAsync(Token token)
        {
            return dbContext.Tokens.AddAsync(token).AsTask();
        }

        public ValueTask<Token?> GetAsync(Guid tokenId)
        {
            return dbContext.Tokens.FindAsync(tokenId);
        }
    }
}
