using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings;

internal class MarkMeetingAttendeeFeeAsPayedCommandHandler(IMeetingRepository meetingRepository)
    : ICommandHandler<MarkMeetingAttendeeFeeAsPayedCommand>
{
    public async Task Handle(MarkMeetingAttendeeFeeAsPayedCommand command, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(command.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {command.MeetingId} was not found");

        meeting.MarkAttendeeFeeAsPaid(new MemberId(command.MemberId));
    }
}