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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private OutboxMessage()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Guid Id { get; set; }

    public DateTime OccurredOn { get; set; }

    public string Type { get; set; }

    public string Data { get; set; }

    public DateTime? ProcessedDate { get; set; }
}