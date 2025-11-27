using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Administration.Domain.Users;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Users;

internal class UserContext(IExecutionContextAccessor executionContextAccessor): IUserContext
{
    public UserId UserId => new(executionContextAccessor.UserId);
}