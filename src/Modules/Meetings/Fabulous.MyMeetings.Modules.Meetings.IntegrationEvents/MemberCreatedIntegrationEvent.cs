using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;

public class MemberCreatedIntegrationEvent(Guid id, DateTime occurredOn, Guid memberId): IntegrationEvent(id, occurredOn)
{
    public Guid MemberId { get; } = memberId;
}