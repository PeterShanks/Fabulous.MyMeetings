namespace Fabulous.MyMeetings.Modules.Administration.Domain.Members.Events;

public class MemberCreatedDomainEvent(MemberId memberId) : DomainEvent
{
    public MemberId MemberId { get; } = memberId;
}