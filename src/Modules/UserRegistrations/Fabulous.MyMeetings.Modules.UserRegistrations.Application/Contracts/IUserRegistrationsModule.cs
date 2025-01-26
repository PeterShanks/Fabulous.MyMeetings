namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

public interface IUserRegistrationsModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

    Task ExecuteCommandAsync(ICommand command);

    Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
}