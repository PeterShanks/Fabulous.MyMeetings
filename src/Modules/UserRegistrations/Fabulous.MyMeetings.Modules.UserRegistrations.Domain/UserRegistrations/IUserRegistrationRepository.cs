namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;

public interface IUserRegistrationRepository
{
    void Add(UserRegistration userRegistration);

    Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId);
    Task<UserRegistration?> GetByIdEmail(string email);
}