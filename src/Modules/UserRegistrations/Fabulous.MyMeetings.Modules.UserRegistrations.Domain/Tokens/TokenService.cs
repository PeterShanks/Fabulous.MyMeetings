using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.AspNetCore.DataProtection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;

public class TokenService(
    IDataProtectionProvider protectionProvider,
    ITokenRepository tokenRepository,
    TimeProvider timeProvider) : ITokenService
{
    private readonly IDataProtector _protector = protectionProvider.CreateProtector("TokenService");
    public async Task<string> CreateAsync(Guid userId, TokenTypeId tokenTypeId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty.", nameof(userId));
        }

        var previousIssuedTokens = await tokenRepository.GetUnusedTokensAsync(userId, tokenTypeId);
        foreach (var previousIssuedToken in previousIssuedTokens)
        {
            previousIssuedToken.Invalidate();
        }

        var tokenId = Guid.NewGuid();
        var memoryStream = new MemoryStream();
        var createdAt = timeProvider.GetUtcNow().UtcDateTime;
        var expiresAt = createdAt.AddMinutes(10);
        await using (var writer = memoryStream.CreateWriter())
        {
            writer.Write(tokenId.ToByteArray());
            writer.Write(userId.ToByteArray());
            writer.Write(createdAt);
            writer.Write(expiresAt);
            writer.Write((int)tokenTypeId);
        }

        var protectedBytes = _protector.Protect(memoryStream.ToArray());
        var tokenString = Convert.ToBase64String(protectedBytes);

        var token = new Token(tokenId, userId, tokenTypeId, tokenString, createdAt, expiresAt);
        await tokenRepository.AddAsync(token);

        return tokenString;
    }

    // ReSharper disable once FlagArgument
    public async Task ValidateAsync(string token, TokenTypeId tokenTypeId, Guid userId)
    {
        if (!await IsValidAsync(token, tokenTypeId, userId))
            throw new InvalidTokenException();
    }

    // ReSharper disable once FlagArgument
    public async Task<bool> IsValidAsync(string token, TokenTypeId tokenTypeId, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be empty.", nameof(token));

        if (userId == Guid.Empty)
            throw new ArgumentException("User id cannot be empty.", nameof(userId));

        try
        {
            var unprotectedData = _protector.Unprotect(Convert.FromBase64String(token));
            var memoryStream = new MemoryStream(unprotectedData);

            using var reader = memoryStream.CreateReader();
            var tokenId = new Guid(reader.ReadBytes(16));

            var tokenUserId = new Guid(reader.ReadBytes(16));
            if (tokenUserId != userId)
                return false;

            _ = reader.ReadDateTime();

            var expiresAt = reader.ReadDateTime();
            if (timeProvider.GetUtcNow().UtcDateTime > expiresAt)
                return false;

            if (tokenTypeId != (TokenTypeId)reader.ReadInt32())
                return false;

            var storedToken = await tokenRepository.GetAsync(tokenId);
            if (storedToken is null)
                return false;

            storedToken.SetAsUsed();
            return true;
        }
        catch(Exception ex) when(ex is not BusinessRuleValidationException)
        {
            return false;
        }
    }
}