using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.Users;

internal class UserRepository(UserAccessContext userAccessContext) : IUserRepository
{
    public void Add(User user)
    {
        userAccessContext.Users.Add(user);
    }
}