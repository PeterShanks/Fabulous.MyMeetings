using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;
using Fabulous.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class MeetingGroupProposalAcceptedNotificationHandler(IEventBus eventBus): INotificationHandler<MeetingGroupProposalAcceptedNotification>
{
    public Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
    {
        return eventBus.Publish(new MeetingGroupProposalAcceptedIntegrationEvent(
            Guid.CreateVersion7(),
            DateTime.UtcNow,
            notification.DomainEvent.MeetingGroupProposalId
        ));
    }
}