using Fabulous.MyMeetings.Modules.Payments.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings;

public class MeetingFeePaidIntegrationEventHandler(ICommandsScheduler commandsScheduler)
    : INotificationHandler<MeetingFeePaidIntegrationEvent>
{
    public async Task Handle(MeetingFeePaidIntegrationEvent @event, CancellationToken cancellationToken)
    {
        await commandsScheduler.EnqueueAsync(new MarkMeetingAttendeeFeeAsPayedCommand(
            Guid.NewGuid(),
            @event.PayerId,
            @event.MeetingId));
    }
}