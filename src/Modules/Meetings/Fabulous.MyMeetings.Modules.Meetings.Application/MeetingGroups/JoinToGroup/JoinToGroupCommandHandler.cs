using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup;

public class JoinToGroupCommandHandler(
    IMeetingGroupRepository meetingGroupRepository,
    IMemberContext memberContext): ICommandHandler<JoinToGroupCommand>
{
    public async Task Handle(JoinToGroupCommand request, CancellationToken cancellationToken)
    {
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

        if (meetingGroup == null)
            throw new InvalidCommandException(["Meeting group must exist."]);

        meetingGroup.JoinToGroupMember(memberContext.MemberId);
    }
}