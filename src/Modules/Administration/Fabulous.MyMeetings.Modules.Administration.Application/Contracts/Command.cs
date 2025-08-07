namespace Fabulous.MyMeetings.Modules.Administration.Application.Contracts;

public abstract class Command : ICommand
{
    public Guid Id { get; } = Guid.CreateVersion7();
}

public abstract class Command<TResult> : ICommand<TResult>
{
    public Guid Id { get; } = Guid.CreateVersion7();
}