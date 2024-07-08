using MediatR;

namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public class UnitOfWorkCommandHandlerDecorator<T> : IRequestHandler<T>
    where T : IRequest
{
    private readonly IRequestHandler<T> _decorated;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(IRequestHandler<T> decorated, IUnitOfWork unitOfWork)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(T request, CancellationToken cancellationToken)
    {
        await _decorated.Handle(request, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}