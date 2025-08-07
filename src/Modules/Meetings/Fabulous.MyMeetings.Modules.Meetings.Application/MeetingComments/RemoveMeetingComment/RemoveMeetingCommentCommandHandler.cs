using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;

public class RemoveMeetingCommentCommandHandler(
    IMeetingCommentRepository meetingCommentRepository,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository,
    IMemberContext memberContext): ICommandHandler<RemoveMeetingCommentCommand>
{
    public async Task Handle(RemoveMeetingCommentCommand request, CancellationToken cancellationToken)
    {
        var meetingComment = 
            await meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));

        if (meetingComment is null)
            throw new InvalidCommandException(["Meeting comment for removing must exist."]);

        var meeting = await meetingRepository.GetByIdAsync(meetingComment.MeetingId);
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        meetingComment.Remove(memberContext.MemberId, meetingGroup);
    }
}