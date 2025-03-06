namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens
{
    public interface ITokenRepository
    {
        Task AddAsync(Token token);
        ValueTask<Token?> GetAsync(Guid tokenId);
        Task<List<Token>> GetUnusedTokensAsync(Guid userId, TokenTypeId tokenTypeId);
    }
}
