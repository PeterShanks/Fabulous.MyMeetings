using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee;

public class RemoveMeetingAttendeeCommandHandler(IMeetingRepository meetingRepository, IMemberContext memberContext) : ICommandHandler<RemoveMeetingAttendeeCommand>
{
    public async Task Handle(RemoveMeetingAttendeeCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with ID '{request.MeetingId}' not found.");

        meeting.RemoveAttendee(new MemberId(request.AttendeeId), memberContext.MemberId, request.RemovingReason);
    }
}