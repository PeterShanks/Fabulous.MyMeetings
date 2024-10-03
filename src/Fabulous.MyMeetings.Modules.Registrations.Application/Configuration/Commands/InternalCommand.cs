using Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;

public abstract class InternalCommand : ICommand
{
    protected InternalCommand()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}

public abstract class InternalCommand<TResult> : ICommand<TResult>
{
    protected InternalCommand()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}