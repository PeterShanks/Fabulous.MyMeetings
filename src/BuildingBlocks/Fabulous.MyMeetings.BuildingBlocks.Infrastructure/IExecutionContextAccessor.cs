namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure;

public interface IExecutionContextAccessor
{
    Guid UserId { get; }

    Guid CorrelationId { get; }

    bool IsAvailable { get; }
}