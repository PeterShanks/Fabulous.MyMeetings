using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Outbox;

internal class OutboxAccessor(AdministrationContext context): IOutbox
{
    public void Add(OutboxMessage message)
    {
        context.OutboxMessages.Add(message);
    }
}