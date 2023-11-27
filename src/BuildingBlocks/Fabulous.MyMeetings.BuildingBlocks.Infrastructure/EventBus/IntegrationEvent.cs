using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    public abstract class IntegrationEvent : INotification
    {
        protected IntegrationEvent(Guid id, DateTime occurredOn)
        {
            OccurredOn = occurredOn;
            Id = id;
        }

        public Guid Id { get; }
        public DateTime OccurredOn { get; }

    }
}
