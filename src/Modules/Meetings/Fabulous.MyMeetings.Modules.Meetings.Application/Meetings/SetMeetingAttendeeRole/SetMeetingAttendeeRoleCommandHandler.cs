using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole;

internal class SetMeetingAttendeeRoleCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository)
    : ICommandHandler<SetMeetingAttendeeRoleCommand>
{
    public async Task Handle(SetMeetingAttendeeRoleCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        var meetingGroup = await meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        meeting.SetAttendeeRole(meetingGroup!, memberContext.MemberId, new MemberId(request.MemberId));
    }
}