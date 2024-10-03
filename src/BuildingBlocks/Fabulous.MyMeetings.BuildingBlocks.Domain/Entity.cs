using System.Collections.ObjectModel;

namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public abstract class Entity
{
    private static readonly IReadOnlyCollection<IDomainEvent> EmptyDomainEvents =
        ReadOnlyCollection<IDomainEvent>.Empty;

    private List<IDomainEvent>? _domainEvents;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly() ?? EmptyDomainEvents;

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    /// <exception cref="BusinessRuleValidationException">Broken rule exception.</exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}