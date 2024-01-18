using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly UserAccessContext _userAccessContext;

        public UserRepository(UserAccessContext userAccessContext)
        {
            _userAccessContext = userAccessContext;
        }

        public void Add(User user)
        {
            _userAccessContext.Users.Add(user);
        }
    }
}
