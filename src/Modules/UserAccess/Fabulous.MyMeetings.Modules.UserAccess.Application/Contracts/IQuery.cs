using MediatR;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}