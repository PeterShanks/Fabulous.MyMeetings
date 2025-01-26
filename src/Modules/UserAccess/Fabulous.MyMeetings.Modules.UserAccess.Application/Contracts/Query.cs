namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

public abstract class Query<TResult> : IQuery<TResult>
{
    public Guid Id { get; } = Guid.NewGuid();
}