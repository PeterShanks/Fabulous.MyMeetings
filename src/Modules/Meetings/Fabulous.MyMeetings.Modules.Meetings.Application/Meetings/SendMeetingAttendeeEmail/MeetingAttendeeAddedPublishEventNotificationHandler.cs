using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;

public class MeetingAttendeeAddedPublishEventNotificationHandler(IEventBus eventBus): INotificationHandler<MeetingAttendeeAddedNotification>
{
    public Task Handle(MeetingAttendeeAddedNotification notification, CancellationToken cancellationToken)
    {
        return eventBus.Publish(new MeetingAttendeeAddedIntegrationEvent(
            Guid.CreateVersion7(),
            notification.DomainEvent.OccurredOn,
            notification.DomainEvent.MeetingId,
            notification.DomainEvent.AttendeeId,
            notification.DomainEvent.FeeValue,
            notification.DomainEvent.FeeCurrency
        ));
    }
}