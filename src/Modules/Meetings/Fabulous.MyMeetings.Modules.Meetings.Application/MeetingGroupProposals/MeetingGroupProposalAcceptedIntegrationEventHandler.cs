using Fabulous.MyMeetings.Modules.Administration.IntegrationEvents.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals;

internal class MeetingGroupProposalAcceptedIntegrationEventHandler(ICommandsScheduler commandsScheduler): INotificationHandler<MeetingGroupProposalAcceptedIntegrationEvent>
{
    public Task Handle(MeetingGroupProposalAcceptedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new AcceptMeetingGroupProposalCommand(
            Guid.CreateVersion7(),
            notification.MeetingGroupProposalId));
    }
}