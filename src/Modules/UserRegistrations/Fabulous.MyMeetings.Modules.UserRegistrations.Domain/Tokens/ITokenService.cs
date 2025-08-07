namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;

public interface ITokenService
{
    Task<string> CreateAsync(Guid userId, TokenTypeId tokenTypeId);
    Task<bool> IsValidAsync(string token, TokenTypeId tokenTypeId, Guid userId);
    Task ValidateAsync(string token, TokenTypeId tokenTypeId, Guid userId);
}