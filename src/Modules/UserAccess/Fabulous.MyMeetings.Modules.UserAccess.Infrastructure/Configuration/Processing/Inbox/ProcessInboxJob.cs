using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxJob : IJob
{
    private readonly IMediator _mediator;

    public ProcessInboxJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Execute(IJobExecutionContext context)
    {
        return _mediator.Send(new ProcessInboxCommand());
    }
}