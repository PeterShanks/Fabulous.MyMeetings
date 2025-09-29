using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole;

public class SetMeetingHostRoleCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository) : ICommandHandler<SetMeetingHostRoleCommand>
{
    public async Task Handle(SetMeetingHostRoleCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingId));

        meeting.SetHostRole(meetingGroup!, memberContext.MemberId, new MemberId(request.MemberId));
    }
}