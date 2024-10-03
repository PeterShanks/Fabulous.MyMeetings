using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

public class GetUserQuery(Guid userId) : Query<UserDto>
{
    public Guid UserId { get; } = userId;
}