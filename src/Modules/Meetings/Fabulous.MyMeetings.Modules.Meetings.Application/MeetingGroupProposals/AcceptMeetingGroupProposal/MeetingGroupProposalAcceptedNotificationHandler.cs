using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

internal class MeetingGroupProposalAcceptedNotificationHandler(ICommandsScheduler commandsScheduler): INotificationHandler<MeetingGroupProposalAcceptedNotification>
{
    public Task Handle(MeetingGroupProposalAcceptedNotification notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new CreateNewMeetingGroupCommand(
            Guid.CreateVersion7(),
            notification.DomainEvent.MeetingGroupProposalId));
    }
}