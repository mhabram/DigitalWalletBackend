using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.ValueObjects;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.Entities;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Savings.Configuration;

internal sealed class SavingConfiguration : IEntityTypeConfiguration<Saving>
{
    public void Configure(EntityTypeBuilder<Saving> builder)
    {
        ConfigureSavingTable(builder);
        ConfigureSavingTransactionTable(builder);
    }

    private static void ConfigureSavingTable(EntityTypeBuilder<Saving> builder)
    {
        builder.ToTable("Savings");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SavingId.Create(value))
            .IsRequired();

        builder.Property(s => s.Balance)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(s => s.Currency)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(s => s.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(s => s.Created)
            .IsRequired();

        builder.Property(s => s.Modified)
            .IsRequired(false);
    }

    private static void ConfigureSavingTransactionTable(EntityTypeBuilder<Saving> builder)
    {
        builder.OwnsMany(
            s => s.Transactions,
            sb =>
        {
            sb.ToTable("SavingTransactions");

            sb.WithOwner()
                .HasForeignKey(nameof(SavingId));

            sb.HasKey(nameof(SavingTransaction.Id), nameof(SavingId));

            sb.Property(s => s.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SavingTransactionId.Create(value));

            sb.Property(s => s.Amount)
                .HasPrecision(10, 2)
                .IsRequired();

            sb.OwnsOne(s => s.DepositType,
                sd => sd.Property(d => d.Value)
                        .HasColumnName(nameof(TransactionType))
                        .IsRequired());

            sb.Property(s => s.Created)
                .IsRequired();
        });
    }
}
