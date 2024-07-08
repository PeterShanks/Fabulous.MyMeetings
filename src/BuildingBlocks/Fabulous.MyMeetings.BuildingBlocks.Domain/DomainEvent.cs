namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public class DomainEvent : IDomainEvent
{
    public DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = TimeProvider.System.GetUtcNow().DateTime;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
}