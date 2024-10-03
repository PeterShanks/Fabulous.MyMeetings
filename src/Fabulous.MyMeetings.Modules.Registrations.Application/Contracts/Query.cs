namespace Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

public abstract class Query<TResult> : IQuery<TResult>
{
    protected Query()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}