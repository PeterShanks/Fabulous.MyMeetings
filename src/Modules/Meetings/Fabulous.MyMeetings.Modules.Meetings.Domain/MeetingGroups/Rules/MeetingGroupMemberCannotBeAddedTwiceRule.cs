using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class MeetingGroupMemberCannotBeAddedTwiceRule(IEnumerable<MeetingGroupMember> members, MemberId memberId): IBusinessRule
{
    public string Message => "Member cannot be added twice to the same group";

    public bool IsBroken() => members.Any(x => x.IsMember(memberId));
}