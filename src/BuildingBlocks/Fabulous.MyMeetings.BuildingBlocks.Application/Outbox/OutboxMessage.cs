namespace Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;

public class OutboxMessage
{
    public OutboxMessage(Guid id, DateTime occurredOn, string type, string data)
    {
        Id = id;
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }

    private OutboxMessage()
    {
    }

    public Guid Id { get; init; }

    public DateTime OccurredOn { get; init; }

    public string Type { get; init; } = null!;

    public string Data { get; init; } = null!;

    public DateTime? ProcessedDate { get; init; }
}