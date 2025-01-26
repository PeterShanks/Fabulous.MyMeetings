using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.UserRegistrations;

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