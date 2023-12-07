using DigitalWallet.Application.Savings.Repositories;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Savings.Repositories;

public sealed class SavingQueryRepository : ISavingQueryRepository
{
    private readonly SavingDbContext _context;

    public SavingQueryRepository(
        SavingDbContext context)
    {
        _context = context;
    }

    public async Task<Saving> GetSavingByIdAsync(SavingId id, CancellationToken cancellationToken)
    {
        return await _context
            .Savings
            .Include(s => s.Transactions)
            .SingleAsync(s => s.Id == id, cancellationToken);
    }
}
