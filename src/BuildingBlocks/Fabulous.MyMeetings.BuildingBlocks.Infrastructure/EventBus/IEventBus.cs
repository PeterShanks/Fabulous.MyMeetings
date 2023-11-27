namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IntegrationEvent;
        void Subscribe<T>(IIntegrationEventHandler<T> handler)
            where T: IntegrationEvent;
    }
}
