using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Rules;

public class MeetingHostMustBeAMeetingGroupMemberRule(
    MemberId creatorId,
    List<MemberId> hostsMemberIds,
    List<MeetingGroupMember> members): IBusinessRule
{
    public string Message => "Meeting host must be a meeting group member";
    public bool IsBroken()
    {
        var memberIds = members.Select(m => m.MemberId).ToList();

        if (!hostsMemberIds.Any() && !memberIds.Contains(creatorId))
        {
            return true;
        }

        return hostsMemberIds.Any() && hostsMemberIds.Except(memberIds).Any();
    }
}