using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Outbox;

internal class OutboxAccessor(RegistrationsContext userAccessContext) : IOutbox
{
    public void Add(OutboxMessage message)
    {
        userAccessContext.OutboxMessages.Add(message);
    }

    public Task Save()
    {
        return
            Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    }
}