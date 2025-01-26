namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

public interface IUsersCounter
{
    int CountUsersWithEmail(string email);
}