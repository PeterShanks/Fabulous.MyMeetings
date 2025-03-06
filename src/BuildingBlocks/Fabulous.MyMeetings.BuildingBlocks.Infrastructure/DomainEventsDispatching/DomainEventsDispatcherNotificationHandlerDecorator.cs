using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

[SkipAutoRegistration]
public class DomainEventsDispatcherNotificationHandlerDecorator<T>(INotificationHandler<T> decorated,
    IDomainEventsDispatcher domainEventsDispatcher) : INotificationHandler<T>
    where T : INotification
{
    public async Task Handle(T notification, CancellationToken cancellationToken)
    {
        await decorated.Handle(notification, cancellationToken);
        await domainEventsDispatcher.DispatchEventsAsync();
    }
}