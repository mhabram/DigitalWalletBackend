namespace DigitalWallet.Domain.Common.Primitives;

public interface IDomainEventEntityBase
{
    IReadOnlyList<IDomainEvent> GetDomainEevents();
    void RaiseDomainEvent(IDomainEvent domaineEvent);
    void ClearDomainEvents();
}
