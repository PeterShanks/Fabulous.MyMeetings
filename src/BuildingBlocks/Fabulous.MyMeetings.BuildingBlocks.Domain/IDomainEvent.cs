using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Domain
{
    public interface IDomainEvent: INotification
    {
        Guid Id { get; }

        DateTime OccurredOn { get; }
    }
}
