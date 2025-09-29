namespace Fabulous.MyMeetings.Modules.Administration.Application.Members.GetMember;

public class GetMemberQuery(Guid memberId): Query<MemberDto>
{
    public Guid MemberId { get; } = memberId;
}