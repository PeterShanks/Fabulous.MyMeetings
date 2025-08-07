using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;

public class EditMeetingCommentCommandHandler(
    IMeetingCommentRepository meetingCommentRepository,
    IMeetingCommentingConfigurationRepository meetingCommentingConfigurationRepository,
    IMemberContext memberContext): ICommandHandler<EditMeetingCommentCommand>
{
    public async Task Handle(EditMeetingCommentCommand request, CancellationToken cancellationToken)
    {
        var meetingComment =
            await meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));

        if (meetingComment is null)
            throw new InvalidCommandException(["Meeting comment for editing must exist."]);

        var meetingCommentingConfiguration = await meetingCommentingConfigurationRepository.GetByMeetingIdAsync(meetingComment.MeetingId);

        meetingComment.Edit(
            memberContext.MemberId,
            request.EditedComment,
            meetingCommentingConfiguration!);
    }
}