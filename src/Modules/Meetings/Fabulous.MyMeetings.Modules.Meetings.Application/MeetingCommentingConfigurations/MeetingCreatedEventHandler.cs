using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations;

internal class MeetingCreatedEventHandler(
    IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
    IMeetingRepository meetingRepository): INotificationHandler<MeetingCreatedDomainEvent>
{
    public async Task Handle(MeetingCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(notification.MeetingId);

        var meetingCommentingConfiguration = meeting.CreateCommentingConfiguration();

        await meetingCommentingConfigurationRepository.AddAsync(meetingCommentingConfiguration);
    }
}