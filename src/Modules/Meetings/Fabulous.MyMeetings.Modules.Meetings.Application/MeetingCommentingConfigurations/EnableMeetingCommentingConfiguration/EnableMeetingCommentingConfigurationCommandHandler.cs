using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration;

public class EnableMeetingCommentingConfigurationCommandHandler(
    IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository,
    IMemberContext memberContext) : ICommandHandler<DisableMeetingCommentingConfigurationCommand>
{
    public async Task Handle(DisableMeetingCommentingConfigurationCommand request,
        CancellationToken cancellationToken)
    {
        var meetingId = new MeetingId(request.MeetingId);
        var meetingCommentingConfiguration =
            await meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meetingId);

        if (meetingCommentingConfiguration is null)
            throw new InvalidCommandException([
                "Meeting commenting configuration for enabling commenting must exist"
            ]);

        var meeting = await meetingRepository.GetByIdAsync(meetingId);

        var meetingGroup = await meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        meetingCommentingConfiguration.EnableCommenting(memberContext.MemberId, meetingGroup);
    }
}