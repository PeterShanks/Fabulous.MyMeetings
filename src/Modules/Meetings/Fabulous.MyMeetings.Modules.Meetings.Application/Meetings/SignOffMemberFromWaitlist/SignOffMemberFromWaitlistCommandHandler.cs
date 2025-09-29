using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SignOffMemberFromWaitlist;

public class SignOffMemberFromWaitlistCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository): ICommandHandler<SignOffMemberFromWaitlistCommand>
{
    public async Task Handle(SignOffMemberFromWaitlistCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        meeting.SignOffMemberFromWaitlist(memberContext.MemberId);
    }
}