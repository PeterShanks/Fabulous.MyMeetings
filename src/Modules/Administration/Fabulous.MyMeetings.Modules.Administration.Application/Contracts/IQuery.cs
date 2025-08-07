using MediatR;

namespace Fabulous.MyMeetings.Modules.Administration.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>;