using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.UserRegistrations
{
    public class UserRegistrationRepository : IUserRegistrationRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRegistrationRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public void Add(UserRegistration userRegistration)
        {
            _userAccessContext.Add(userRegistration);
        }

        public Task<UserRegistration?> GetByIdAsync(UserRegistrationId userRegistrationId)
        {
            return _userAccessContext.UserRegistrations.FirstOrDefaultAsync(x => x.Id == userRegistrationId);
        }
    }
}
