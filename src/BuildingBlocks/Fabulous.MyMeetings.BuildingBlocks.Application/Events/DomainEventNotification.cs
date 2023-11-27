using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Events
{
    public class DomainEventNotification<T> : IDomainEventNotification<T>
        where T : IDomainEvent
    {
        public Guid Id { get; }
        public T DomainEvent { get; }

        public DomainEventNotification(T domainEvent, Guid id)
        {
            Id = id;
            DomainEvent = domainEvent;
        }
    }
}
