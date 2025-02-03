using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.UserRegistrations;

internal class UserRegistrationRepository(UserRegistrationsContext userRegistrationsContext) : IUserRegistrationRepository
{
    public void Add(UserRegistration userRegistration)
    {
        userRegistrationsContext.Add(userRegistration);
    }

    public Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
    {
        return userRegistrationsContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
    }
}