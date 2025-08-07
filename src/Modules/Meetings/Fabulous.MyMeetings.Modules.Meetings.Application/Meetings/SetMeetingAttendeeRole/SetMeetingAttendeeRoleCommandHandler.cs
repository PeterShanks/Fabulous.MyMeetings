using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole;

internal class SetMeetingAttendeeRoleCommandHandler : ICommandHandler<SetMeetingAttendeeRoleCommand>
{
    private readonly IMemberContext _memberContext;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IMeetingGroupRepository _meetingGroupRepository;

    internal SetMeetingAttendeeRoleCommandHandler(
        IMemberContext memberContext,
        IMeetingRepository meetingRepository,
        IMeetingGroupRepository meetingGroupRepository)
    {
        _memberContext = memberContext;
        _meetingRepository = meetingRepository;
        _meetingGroupRepository = meetingGroupRepository;
    }

    public async Task Handle(SetMeetingAttendeeRoleCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        var meetingGroup = await _meetingGroupRepository.GetByIdAsync(meeting.MeetingGroupId);

        meeting.SetAttendeeRole(meetingGroup!, _memberContext.MemberId, new MemberId(request.MemberId));
    }
}