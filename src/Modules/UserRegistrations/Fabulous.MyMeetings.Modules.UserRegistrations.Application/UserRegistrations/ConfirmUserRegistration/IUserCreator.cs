namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;

public interface IUserCreator
{
    public Task Create(
        Guid userRegistrationId,
        string password,
        string email,
        string firstName,
        string lastName);
}