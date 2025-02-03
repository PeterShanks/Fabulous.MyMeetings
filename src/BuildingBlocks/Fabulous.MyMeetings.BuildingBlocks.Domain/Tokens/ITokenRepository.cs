namespace Fabulous.MyMeetings.BuildingBlocks.Domain.Tokens
{
    public interface ITokenRepository
    {
        Task AddAsync(Token token);
        ValueTask<Token?> GetAsync(Guid tokenId);
    }
}
