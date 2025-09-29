using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply;

public class AddReplyToMeetingCommentCommandHandler(
    IMeetingCommentRepository meetingCommentRepository,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository,
    IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
    IMemberContext memberContext): ICommandHandler<AddReplyToMeetingCommentCommand, Guid>
{
    public async Task<Guid> Handle(AddReplyToMeetingCommentCommand request, CancellationToken cancellationToken)
    {
        var meetingComment =
            await meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.InReplyToComment));

        if (meetingComment is null)
            throw new InvalidCommandException(["To create reply the comment must exist."]);

        var meeting = await meetingRepository.GetByIdAsync(meetingComment.MeetingId);

        var meetingGroup = await meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        var meetingCommentingConfiguration =
            await meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meeting.Id);

        var replyToComment = meetingComment.Reply(
            memberContext.MemberId,
            request.Reply,
            meetingGroup!,
            meetingCommentingConfiguration!);

        await meetingCommentRepository.AddAsync(replyToComment);

        return replyToComment.Id.Value;
    }
}