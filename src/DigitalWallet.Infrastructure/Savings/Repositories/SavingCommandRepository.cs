using DigitalWallet.Application.Savings.Repositories.Commands;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;

namespace DigitalWallet.Infrastructure.Savings.Repositories;

public sealed class SavingCommandRepository : ISavingCommandRepository
{
    private readonly SavingDbContext _context;

    public SavingCommandRepository(
        SavingDbContext context)
    {
        _context = context;
    }

    public async Task UpdateSavingAsync(Saving saving, CancellationToken cancellationToken)
    {
        _context.Update(saving);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateNewSavingAsync(Saving saving, CancellationToken cancellationToken)
    {
        await _context.Savings.AddAsync(saving, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
