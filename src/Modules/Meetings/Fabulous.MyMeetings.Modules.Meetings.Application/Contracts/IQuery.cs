using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;

public interface IQuery<out TResult> : IRequest<TResult>;