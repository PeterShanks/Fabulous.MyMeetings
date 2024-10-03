using MediatR;

namespace Fabulous.MyMeetings.Modules.Registrations.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>
{
    Guid Id { get; }
}