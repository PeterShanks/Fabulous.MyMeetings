using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Authentication;

public class MemberContext(IExecutionContextAccessor executionContextAccessor): IMemberContext
{
    public MemberId MemberId => new(executionContextAccessor.UserId);
}