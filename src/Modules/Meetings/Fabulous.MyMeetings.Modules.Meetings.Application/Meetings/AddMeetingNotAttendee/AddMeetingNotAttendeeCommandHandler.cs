using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingNotAttendee;

public class AddMeetingNotAttendeeCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository): ICommandHandler<AddMeetingNotAttendeeCommand>
{
    public async Task Handle(AddMeetingNotAttendeeCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        meeting.AddNotAttendee(memberContext.MemberId);
    }
}