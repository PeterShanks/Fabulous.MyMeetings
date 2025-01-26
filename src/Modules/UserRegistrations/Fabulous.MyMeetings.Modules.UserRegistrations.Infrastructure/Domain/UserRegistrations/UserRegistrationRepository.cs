using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationRepository(RegistrationsContext context) : IUserRegistrationRepository
    {
        public void Add(UserRegistration userRegistration)
        {
            context.Add(userRegistration);
        }

        public async Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
            return await context.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}
