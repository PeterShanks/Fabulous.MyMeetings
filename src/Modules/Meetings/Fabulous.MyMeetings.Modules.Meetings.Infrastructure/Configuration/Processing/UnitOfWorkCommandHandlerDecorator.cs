using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
internal class UnitOfWorkCommandHandlerDecorator<T>(ICommandHandler<T> decorated, IUnitOfWork unitOfWork,
    MeetingsContext context,
    TimeProvider timeProvider) : ICommandHandler<T>
    where T : ICommand
{
    public async Task Handle(T request, CancellationToken cancellationToken)
    {
        await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand)
        {
            var internalCommand = await context.InternalCommands.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = timeProvider.GetUtcNow().UtcDateTime;
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);
    }
}