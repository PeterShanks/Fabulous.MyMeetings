using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public class UnitOfWork(DbContext context, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
{
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        await domainEventsDispatcher.DispatchEventsAsync();

        return await context.SaveChangesAsync(cancellationToken);
    }
}