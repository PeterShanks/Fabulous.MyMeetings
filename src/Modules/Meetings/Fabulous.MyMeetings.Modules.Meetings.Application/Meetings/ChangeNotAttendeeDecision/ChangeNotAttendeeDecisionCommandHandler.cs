using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.ChangeNotAttendeeDecision;

public class ChangeNotAttendeeDecisionCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository): ICommandHandler<ChangeNotAttendeeDecisionCommand>
{
    public async Task Handle(ChangeNotAttendeeDecisionCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");
            
        meeting.ChangeNotAttendeeDecision(memberContext.MemberId);
    }
}