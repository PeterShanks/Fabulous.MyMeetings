namespace Fabulous.MyMeetings.BuildingBlocks.Domain
{
    public class DomainEvent: IDomainEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = TimeProvider.System.GetUtcNow().DateTime;
        }
    }
}
