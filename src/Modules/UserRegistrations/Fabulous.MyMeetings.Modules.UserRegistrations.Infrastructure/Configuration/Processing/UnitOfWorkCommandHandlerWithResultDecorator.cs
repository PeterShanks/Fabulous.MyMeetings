using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing;

internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>(ICommandHandler<T, TResult> decorated, IUnitOfWork unitOfWork,
    UserRegistrationsContext context) : ICommandHandler<T, TResult>
    where T : ICommand<TResult>
{
    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
        var result = await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand<TResult> internalCommand)
            await context.InternalCommands
                .Where(c => c.Id == internalCommand.Id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.ProcessedDate, DateTime.UtcNow),
                    cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}