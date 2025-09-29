using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CancelMeeting;

public class CancelMeetingCommandHandler(
    IMeetingRepository meetingRepository,
    IMemberContext memberContext): ICommandHandler<CancelMeetingCommand>
{
    public async Task Handle(CancelMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        meeting.Cancel(memberContext.MemberId);
    }
}