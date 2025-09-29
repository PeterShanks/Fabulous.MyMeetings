using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail;

public class SendMeetingGroupCreatedEmailCommand(
    Guid id,
    MeetingGroupId meetingGroupId,
    MemberId creatorId): InternalCommand(id)
{
    public MeetingGroupId MeetingGroupId { get; } = meetingGroupId;
    public MemberId CreatorId { get; } = creatorId;
}