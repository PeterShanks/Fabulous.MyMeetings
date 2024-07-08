using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

public class GetUserQuery : Query<UserDto>
{
    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}