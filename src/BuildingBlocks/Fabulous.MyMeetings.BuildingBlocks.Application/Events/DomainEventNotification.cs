using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Events;

public abstract class DomainEventNotification<T> : IDomainEventNotification<T>
    where T : IDomainEvent
{
    protected DomainEventNotification(T domainEvent, Guid id)
    {
        Id = id;
        DomainEvent = domainEvent;
    }

    public Guid Id { get; }
    public T DomainEvent { get; }
}