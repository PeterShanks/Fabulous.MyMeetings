namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;

public interface IUserRegistrationRepository
{
    void Add(UserRegistration userRegistration);

    Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId);
}