using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DependencyInjection;
using Fabulous.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration.Processing;

[SkipAutoRegistration]
internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>(ICommandHandler<T, TResult> decorated, IUnitOfWork unitOfWork,
    MeetingsContext context,
    TimeProvider timeProvider) : ICommandHandler<T, TResult>
    where T : ICommand<TResult>
{
    public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
    {
        var result = await decorated.Handle(request, cancellationToken);

        if (request is InternalCommand<TResult>)
        {
            var internalCommand = await context.InternalCommands.FindAsync([request.Id], cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = timeProvider.GetUtcNow().UtcDateTime;
            }
        }

        await unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}