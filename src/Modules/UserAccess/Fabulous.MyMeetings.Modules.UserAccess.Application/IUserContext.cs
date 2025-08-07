using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application;

public interface IUserContext
{
    UserId UserId { get; }
}