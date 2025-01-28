namespace Fabulous.MyMeetings.BuildingBlocks.Application.PasswordManager;

public interface IPasswordManager
{
    string HashPassword(string password);
    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}