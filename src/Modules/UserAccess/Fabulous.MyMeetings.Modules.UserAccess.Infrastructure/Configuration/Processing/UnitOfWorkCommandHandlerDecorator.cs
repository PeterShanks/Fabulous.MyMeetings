using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;

internal class UnitOfWorkCommandHandlerDecorator<T>(ICommandHandler<T> decorated, IUnitOfWork unitOfWork,
    UserAccessContext context) : ICommandHandler<T>
    where T : ICommand
{
    public async Task Handle(T request, CancellationToken cancellationToken)
    {
        await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand internalCommand)
            await context.InternalCommands
                .Where(c => c.Id == internalCommand.Id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.ProcessedDate, DateTime.UtcNow),
                    cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}