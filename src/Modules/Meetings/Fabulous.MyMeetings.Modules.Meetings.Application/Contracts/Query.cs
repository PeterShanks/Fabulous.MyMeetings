namespace Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;

public abstract class Query<TResult> : IQuery<TResult>
{
    public Guid Id { get; } = Guid.CreateVersion7();
}

public abstract class PagedQuery<TResult>(int? take, int? skip) : Query<IEnumerable<TResult>>
{
    private int? _take = take;
    private int? _skip = skip;
    public int Take
    {
        get => _take.HasValue 
            ? _take.Value > 0
                ? _take.Value
                : 10
            : 10;
        set => _take = value;
    }

    public int Skip
    {
        get => _skip.HasValue
            ? _skip.Value >= 0
                ? _skip.Value
                : 0
            : 0;
        set => _skip = value;
    }
}