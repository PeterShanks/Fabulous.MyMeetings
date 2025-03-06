using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
internal class UnitOfWorkCommandHandlerDecorator<T>(ICommandHandler<T> decorated, IUnitOfWork unitOfWork,
    UserAccessContext context,
    TimeProvider timeProvider) : ICommandHandler<T>
    where T : ICommand
{
    public async Task Handle(T request, CancellationToken cancellationToken)
    {
        await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand)
        {
            var internalCommand = await context.InternalCommands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = timeProvider.GetUtcNow().UtcDateTime;
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);
    }
}