using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;

public class AddMeetingCommentCommandHandler(
    IMeetingRepository meetingRepository,
    IMeetingCommentRepository meetingCommentRepository,
    IMeetingGroupRepository meetingGroupRepository,
    IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
    IMemberContext memberContext): ICommandHandler<AddMeetingCommentCommand, Guid>
{
    public async Task<Guid> Handle(AddMeetingCommentCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));
        if (meeting is null)
            throw new InvalidCommandException(["Meeting for adding comment must exist"]);

        var meetingGroup = await meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        var meetingCommentingConfiguration = await meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meeting.Id);

        var meetingComment = meeting.AddComment(
            memberContext.MemberId,
            request.Comment,
            meetingGroup,
            meetingCommentingConfiguration);

        await meetingCommentRepository.AddAsync(meetingComment);

        return meetingComment.Id.Value;
    }
}