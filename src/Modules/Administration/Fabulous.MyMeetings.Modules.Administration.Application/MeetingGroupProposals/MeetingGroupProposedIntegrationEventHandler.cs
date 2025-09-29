using Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;
using Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals;

public class MeetingGroupProposedIntegrationEventHandler(ICommandsScheduler commandsScheduler): INotificationHandler<MeetingGroupProposedIntegrationEvent>
{
    public Task Handle(MeetingGroupProposedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(new RequestMeetingGroupProposalVerificationCommand(
            Guid.CreateVersion7(),
            notification.MeetingGroupProposalId,
            notification.Name,
            notification.Description,
            notification.LocationCity,
            notification.LocationCountryCode,
            notification.ProposalUserId,
            notification.ProposalDate));
    }
}