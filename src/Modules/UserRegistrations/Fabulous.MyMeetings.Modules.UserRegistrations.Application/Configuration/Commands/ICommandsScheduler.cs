namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(InternalCommand command);
    Task EnqueueAsync<T>(InternalCommand<T> command);
}