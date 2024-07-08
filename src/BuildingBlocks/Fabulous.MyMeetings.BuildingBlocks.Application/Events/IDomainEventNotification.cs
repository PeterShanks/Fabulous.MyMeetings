using Fabulous.MyMeetings.BuildingBlocks.Domain;
using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Events;

public interface IDomainEventNotification : INotification
{
    Guid Id { get; }
}

public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    where TEventType : IDomainEvent
{
    TEventType DomainEvent { get; }
}