using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Outbox;

internal class OutboxAccessor(UserRegistrationsContext userRegistrationsContext) : IOutbox
{
    public void Add(OutboxMessage message)
    {
        userRegistrationsContext.OutboxMessages.Add(message);
    }

    public Task Save()
    {
        return
            Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
    }
}