using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals;

public class MeetingGroupProposedNotificationHandler(IEventBus eventBus): INotificationHandler<MeetingGroupProposedNotification>
{
    public Task Handle(MeetingGroupProposedNotification notification, CancellationToken cancellationToken)
    {
        return eventBus.Publish(new MeetingGroupProposedIntegrationEvent(
            notification.Id,
            notification.DomainEvent.OccurredOn,
            notification.DomainEvent.MeetingGroupProposalId,
            notification.DomainEvent.Name,
            notification.DomainEvent.Description,
            notification.DomainEvent.LocationCity,
            notification.DomainEvent.LocationCountryCode,
            notification.DomainEvent.ProposalUserId,
            notification.DomainEvent.ProposalDate));
    }
}