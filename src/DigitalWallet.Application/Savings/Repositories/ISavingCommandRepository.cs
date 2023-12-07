using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;

namespace DigitalWallet.Application.Savings.Repositories.Commands;

public interface ISavingCommandRepository
{
    Task CreateNewSavingAsync(Saving saving, CancellationToken cancellationToken);
    Task UpdateSavingAsync(Saving saving, CancellationToken cancellationToken);
}
