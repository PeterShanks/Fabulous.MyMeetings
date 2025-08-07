using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;

public interface ICommand: IRequest
{
    Guid Id { get; }
}

public interface ICommand<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}