using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public class DomainEventsAccessor(DbContext dbContext) : IDomainEventsAccessor
{
    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
    {
        var domainEntities = dbContext
            .ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        return domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
    }

    public void ClearAllDomainEvents()
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}