namespace Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(InternalCommand command);
    Task EnqueueAsync<T>(InternalCommand<T> command);
}