using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;

public class SetMeetingGroupExpirationDateCommandHandler(IMeetingGroupRepository meetingGroupRepository)
    : ICommandHandler<SetMeetingGroupExpirationDateCommand>
{
    public async Task Handle(SetMeetingGroupExpirationDateCommand request, CancellationToken cancellationToken)
    {
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

        meetingGroup!.SetExpirationDate(request.DateTo);
    }
}