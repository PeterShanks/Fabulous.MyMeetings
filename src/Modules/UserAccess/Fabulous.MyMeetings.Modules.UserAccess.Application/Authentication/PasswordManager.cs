using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

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

    public virtual PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword);
        ArgumentNullException.ThrowIfNull(providedPassword);

        var decodedHashedPassword = Convert.FromBase64String(hashedPassword);

        if (decodedHashedPassword.Length == 0)
            return PasswordVerificationResult.Failed;

        return decodedHashedPassword[0] switch
        {
            0x00 => VerifyHashedPasswordV2(decodedHashedPassword, providedPassword)
                ? _compatibilityMode == PasswordHasherCompatibilityMode.IdentityV3
                    ? PasswordVerificationResult.SuccessRehashNeeded
                    : PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed,
            0x01 => VerifyHashedPasswordV3(decodedHashedPassword, providedPassword, out var embeddedIterationCount)
                ? embeddedIterationCount < _iterationCount
                    ? PasswordVerificationResult.SuccessRehashNeeded
                    : PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed,
            _ => PasswordVerificationResult.Failed
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

    private static bool VerifyHashedPasswordV2(byte[] hashedPassword, string password)
    {
        const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
        const int Pbkdf2IterCount = 1000; // default for Rfc2898DeriveBytes
        const int Pbkdf2SubkeyLength = 256 / 8; // 256 bits
        const int SaltSize = 128 / 8; // 128 bits

        // We know ahead of time the exact length of a valid hashed password payload.
        if (hashedPassword.Length != 1 + SaltSize + Pbkdf2SubkeyLength) return false; // bad size

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashedPassword, 1, salt, 0, salt.Length);

        var expectedSubkey = new byte[Pbkdf2SubkeyLength];
        Buffer.BlockCopy(hashedPassword, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

        var actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
        return ByteArraysEqual(actualSubkey, expectedSubkey);
    }

    private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password, out int iterCount)
    {
        iterCount = default;

        try
        {
            // Read header information
            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            var saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8) return false;
            var salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            var subkeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8) return false;
            var expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            var actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
            return ByteArraysEqual(actualSubkey, expectedSubkey);
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null) return true;
        if (a == null || b == null || a.Length != b.Length) return false;
        var areSame = true;
        for (var i = 0; i < a.Length; i++) areSame &= a[i] == b[i];
        return areSame;
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)buffer[offset + 0] << 24)
               | ((uint)buffer[offset + 1] << 16)
               | ((uint)buffer[offset + 2] << 8)
               | buffer[offset + 3];
    }
}