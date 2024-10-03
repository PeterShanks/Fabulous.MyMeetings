using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

public class PasswordManager : IPasswordManager
{
    private readonly PasswordHasherCompatibilityMode _compatibilityMode;
    private readonly int _iterationCount;
    private readonly RandomNumberGenerator _rng;

    /// <exception cref="InvalidOperationException">Invalid Password Hasher Iteration Count</exception>
    public PasswordManager(IOptions<PasswordHasherOptions>? optionsAccessor = null)
    {
        var options = optionsAccessor?.Value ?? new PasswordHasherOptions();

        _compatibilityMode = options.CompatibilityMode;
        switch (_compatibilityMode)
        {
            case PasswordHasherCompatibilityMode.IdentityV2:
                // nothing else to do
                break;

            case PasswordHasherCompatibilityMode.IdentityV3:
                _iterationCount = options.IterationCount;
                if (_iterationCount < 1) throw new InvalidOperationException("Invalid Password Hasher Iteration Count");
                break;

            default:
                throw new InvalidOperationException("Invalid Password Hasher Compatibility Mode");
        }

        _rng = options.Rng;
    }

    public virtual string HashPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        return _compatibilityMode switch
        {
            PasswordHasherCompatibilityMode.IdentityV2 => Convert.ToBase64String(HashPasswordV2(password, _rng)),
            PasswordHasherCompatibilityMode.IdentityV3 => Convert.ToBase64String(HashPasswordV3(password, _rng)),
            _ => throw new InvalidOperationException("Invalid Password Hasher Compatibility Mode")
        };
    }

    private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
    {
        const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
        const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
        const int SaltSize = 128 / 8; // 128 bits

        var salt = new byte[SaltSize];
        rng.GetBytes(salt);
        var subKey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

        var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subKey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);
        return outputBytes;
    }

    private byte[] HashPasswordV3(string password, RandomNumberGenerator rng)
    {
        return HashPasswordV3(password, rng, KeyDerivationPrf.HMACSHA256, _iterationCount, 128 / 8, 256 / 8);
    }

    private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf,
        int iterationCount, int saltSize, int numbBytesRequested)
    {
        var salt = new byte[saltSize];
        rng.GetBytes(salt);
        var subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, numbBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subKey.Length];
        outputBytes[0] = 0x01;
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterationCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);
        return outputBytes;
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
}