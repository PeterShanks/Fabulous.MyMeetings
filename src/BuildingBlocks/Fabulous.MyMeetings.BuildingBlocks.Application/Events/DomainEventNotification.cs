using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Events;

public abstract class DomainEventNotification<T>(T domainEvent, Guid id) : IDomainEventNotification<T>
    where T : IDomainEvent
{
    public Guid Id { get; } = id;
    public T DomainEvent { get; } = domainEvent;
}