using MediatR;
using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Events
{
    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
    public interface IDomainEventNotification<out TEventType>: IDomainEventNotification
        where TEventType : IDomainEvent
    {
        TEventType DomainEvent { get; }
    }
}
