using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember;

public class ChangeSubscriptionExpirationDateForMemberCommand(
    Guid id, MemberId memberId, DateTime expirationDate): InternalCommand(id)
{
    public MemberId MemberId { get; } = memberId;
    public DateTime ExpirationDate { get; } = expirationDate;
}