using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

public abstract class InternalCommand : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}

public abstract class InternalCommand<TResult> : ICommand<TResult>
{
    public Guid Id { get; } = Guid.NewGuid();
}