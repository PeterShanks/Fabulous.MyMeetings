namespace Fabulous.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

public abstract class InternalCommand(Guid id): ICommand
{
    public Guid Id { get; } = id;
}

public abstract class InternalCommand<TResult>(Guid id) : ICommand<TResult>
{
    public Guid Id { get; } = id;
}