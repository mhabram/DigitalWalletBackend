using DigitalWallet.Infrastructure.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Infrastructure.Savings;

public sealed class SavingDbContextInitializer : IInitializer
{
    private readonly ILogger<SavingDbContextInitializer> _logger;
    private readonly SavingDbContext _context;

    public SavingDbContextInitializer(
        ILogger<SavingDbContextInitializer> logger,
        SavingDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
