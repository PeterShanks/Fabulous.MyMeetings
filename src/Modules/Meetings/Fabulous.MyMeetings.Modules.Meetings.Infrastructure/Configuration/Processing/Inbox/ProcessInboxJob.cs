using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing.Inbox;

[DisallowConcurrentExecution]
public class ProcessInboxJob(IMediator mediator) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return mediator.Send(new ProcessInboxCommand());
    }
}