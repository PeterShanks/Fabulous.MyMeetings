using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups;

internal class MeetingGroupCreatedNotificationHandler(ICommandsScheduler commandsScheduler): INotificationHandler<MeetingGroupCreatedNotification>
{
    public Task Handle(MeetingGroupCreatedNotification notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new SendMeetingGroupCreatedEmailCommand(
            Guid.CreateVersion7(), 
            notification.DomainEvent.MeetingGroupId,
            notification.DomainEvent.CreatorId));
    }
}