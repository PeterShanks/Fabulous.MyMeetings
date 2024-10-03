namespace Fabulous.MyMeetings.Modules.Registrations.Application.Configuration.Commands;

public interface ICommandsScheduler
{
    Task EnqueueAsync(InternalCommand command);
    Task EnqueueAsync<T>(InternalCommand<T> command);
}