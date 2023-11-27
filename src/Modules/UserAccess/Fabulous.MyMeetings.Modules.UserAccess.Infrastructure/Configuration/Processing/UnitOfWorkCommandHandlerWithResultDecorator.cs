using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult>: ICommandHandler<T, TResult>
        where T: ICommand<TResult>
    {
        private readonly ICommandHandler<T, TResult> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserAccessContext _context;

        public UnitOfWorkCommandHandlerWithResultDecorator(ICommandHandler<T, TResult> decorated, IUnitOfWork unitOfWork, UserAccessContext context)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<TResult> Handle(T request, CancellationToken cancellationToken)
        {
            var result =await _decorated.Handle(request, cancellationToken);

            if (request is InternalCommand<TResult> internalCommand)
            {
                await _context.InternalCommands
                    .Where(c => c.Id == internalCommand.Id)
                    .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.ProcessedDate, DateTime.UtcNow), cancellationToken);
            }

            await _unitOfWork.CommitAsync(cancellationToken);

            return result;
        }
    }
}
