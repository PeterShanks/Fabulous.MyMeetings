namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Authentication;

public interface IPasswordManager
{
    string HashPassword(string password);
    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}