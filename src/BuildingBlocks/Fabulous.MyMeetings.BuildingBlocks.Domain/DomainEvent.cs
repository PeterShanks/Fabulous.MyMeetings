namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public class DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = TimeProvider.System.GetUtcNow().DateTime;
}