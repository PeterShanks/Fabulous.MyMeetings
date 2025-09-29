using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Outbox;

internal class OutboxAccessor(MeetingsContext context) : IOutbox
{
    public void Add(OutboxMessage message)
    {
        context.OutboxMessages.Add(message);
    }
}