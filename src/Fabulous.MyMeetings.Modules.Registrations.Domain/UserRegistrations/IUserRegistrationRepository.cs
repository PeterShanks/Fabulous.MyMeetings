namespace Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;

public interface IUserRegistrationRepository
{
    void Add(UserRegistration userRegistration);

    Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId);
}