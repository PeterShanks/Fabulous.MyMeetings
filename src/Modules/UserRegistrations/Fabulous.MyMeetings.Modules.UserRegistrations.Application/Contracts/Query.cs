namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

public abstract class Query<TResult> : IQuery<TResult>
{
    public Guid Id { get; } = Guid.CreateVersion7();
}