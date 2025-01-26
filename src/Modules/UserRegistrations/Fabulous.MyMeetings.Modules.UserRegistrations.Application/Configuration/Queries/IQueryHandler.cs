using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using MediatR;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Queries;

public interface IQueryHandler<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
}