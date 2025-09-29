using MediatR;
using Quartz;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;

[DisallowConcurrentExecution]
public class ProcessInternalCommandsJob(IMediator mediator) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return mediator.Send(new ProcessInternalCommandsCommand());
    }
}