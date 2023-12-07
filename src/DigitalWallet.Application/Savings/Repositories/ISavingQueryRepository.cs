using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;

namespace DigitalWallet.Application.Savings.Repositories;

public interface ISavingQueryRepository
{
    Task<Saving> GetSavingByIdAsync(SavingId id, CancellationToken cancellationToken);
}
