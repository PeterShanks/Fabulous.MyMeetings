using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserAccess.Application;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Authentication;

internal class UserContext(IExecutionContextAccessor executionContextAccessor): IUserContext
{
    public UserId UserId => new(executionContextAccessor.UserId);
}