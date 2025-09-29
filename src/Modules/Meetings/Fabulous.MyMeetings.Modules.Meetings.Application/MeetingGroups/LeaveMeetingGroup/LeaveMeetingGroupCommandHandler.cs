using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.LeaveMeetingGroup;

public class LeaveMeetingGroupCommandHandler(
    IMeetingGroupRepository meetingGroupRepository,
    IMemberContext memberContext): ICommandHandler<LeaveMeetingGroupCommand>
{
    public async Task Handle(LeaveMeetingGroupCommand request, CancellationToken cancellationToken)
    {
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

        if (meetingGroup == null)
            throw new InvalidCommandException(["Meeting group must exist."]);

        meetingGroup.LeaveGroup(memberContext.MemberId);
    }
}