using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure.Configuration.Domain.UserRegistrations;

internal class UserRegistrationRepository(RegistrationsContext registrationsContext) : IUserRegistrationRepository
{
    public void Add(UserRegistration userRegistration)
    {
        registrationsContext.Add(userRegistration);
    }

    public Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return registrationsContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
    }
}