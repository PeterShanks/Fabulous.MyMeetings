using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;

public class MeetingAttendeeAddedNotificationHandler(ICommandsScheduler commandsScheduler): INotificationHandler<MeetingAttendeeAddedNotification>
{
    public Task Handle(MeetingAttendeeAddedNotification notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new SendMeetingAttendeeAddedEmailCommand(
            Guid.CreateVersion7(),
            notification.DomainEvent.AttendeeId,
            notification.DomainEvent.MeetingId)
        );
    }
}