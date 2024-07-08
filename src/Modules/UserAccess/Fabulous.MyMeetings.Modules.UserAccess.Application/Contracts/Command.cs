namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

public abstract class Command : ICommand
{
    protected Command()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}

public abstract class Command<TResult> : ICommand<TResult>
{
    protected Command()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}