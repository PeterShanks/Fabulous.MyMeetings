using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>;