namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}