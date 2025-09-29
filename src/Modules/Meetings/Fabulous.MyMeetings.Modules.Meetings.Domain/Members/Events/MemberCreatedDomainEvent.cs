using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Members.Events;

public class MemberCreatedDomainEvent(MemberId memberId) : DomainEvent
{
    public MemberId MemberId { get; } = memberId;
}