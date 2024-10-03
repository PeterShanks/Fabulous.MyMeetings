namespace Fabulous.MyMeetings.Modules.Registrations.Application.UserRegistrations.RegisterNewUser;

public interface IPasswordManager
{
    string HashPassword(string password);
}