using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Administration.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>(ICommandHandler<T, TResult> decorated, IUnitOfWork unitOfWork,
    AdministrationContext context,
    TimeProvider timeProvider) : ICommandHandler<T, TResult>
    where T : ICommand<TResult>
{
    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
        var result = await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand<TResult>)
        {
            var internalCommand = await context.InternalCommands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = timeProvider.GetUtcNow().UtcDateTime;
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}