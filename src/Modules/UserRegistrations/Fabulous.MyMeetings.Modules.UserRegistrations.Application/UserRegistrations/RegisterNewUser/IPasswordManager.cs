namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.RegisterNewUser;

public interface IPasswordManager
{
    string HashPassword(string password);
}