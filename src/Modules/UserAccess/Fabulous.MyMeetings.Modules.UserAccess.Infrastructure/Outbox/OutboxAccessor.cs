using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Outbox;

internal class OutboxAccessor(UserAccessContext userAccessContext) : IOutbox
{
    public void Add(OutboxMessage message)
    {
        userAccessContext.OutboxMessages.Add(message);
    }
}