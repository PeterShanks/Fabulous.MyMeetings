using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

public interface IQueryHandler<in TQuery, TResult>:
    IRequestHandler<TQuery, TResult>
    where TQuery: IQuery<TResult>;