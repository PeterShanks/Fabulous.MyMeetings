namespace Fabulous.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(InternalCommand command);
    Task EnqueueAsync<T>(InternalCommand<T> command);
}