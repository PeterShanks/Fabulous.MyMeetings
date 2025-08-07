using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class NotActualGroupMemberCannotLeaveGroupRule(
    IEnumerable<MeetingGroupMember> members,
    MemberId memberId
) : IBusinessRule
{
    public string Message => "Member is not member of this group so he cannot leave it";

    public bool IsBroken()
        => !members.Any(m => m.IsMember(memberId));
}