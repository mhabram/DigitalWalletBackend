using DigitalWallet.Domain.Common.Exceptions;

namespace DigitalWallet.Domain.Common.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId>, IDomainEventEntityBase
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(TId id) : base(id)
    {
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    protected void CheckDomainRule(IDomainRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<IDomainEvent> GetDomainEevents()
    {
        return _domainEvents.AsReadOnly();
    }
}
