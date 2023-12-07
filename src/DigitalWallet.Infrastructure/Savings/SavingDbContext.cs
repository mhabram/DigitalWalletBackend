using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using DigitalWallet.Infrastructure.Savings.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Savings;

public sealed class SavingDbContext : DbContext
{
    private readonly string _schema = "Saving";

    public SavingDbContext(DbContextOptions<SavingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Saving> Savings => Set<Saving>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(_schema);

        builder.ApplyConfiguration(new SavingConfiguration());

        base.OnModelCreating(builder);
    }
}
