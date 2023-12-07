using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Common.Primitives;
using DigitalWallet.Domain.Common.ValueObjects;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;

namespace DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.Entities;

public sealed class SavingTransaction : AggregateRoot<SavingTransactionId>
{
    #region Ctor for migrations
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public SavingTransaction(SavingTransactionId id) : base(id)
    {
        //For migrations only    
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion

    private SavingTransaction(
        SavingTransactionId id,
        decimal amount,
        TransactionType depositType,
        DateTime created)
        : base(id)
    {
        Amount = amount;
        DepositType = depositType;
        Created = created;
    }

    public decimal Amount { get; private set; }
    public TransactionType DepositType { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    public static SavingTransaction CreateNewDeposit(
        decimal amount,
        IDateTimeProvider dateTimeProvider)
    {
        return new(
            SavingTransactionId.CreateUnique(),
            Math.Abs(amount),
            TransactionType.Deposit,
            dateTimeProvider.UtcNow);
    }

    public static SavingTransaction CreateNewWithdraw(
        decimal amount,
        IDateTimeProvider dateTimeProvider)
    {
        return new(
            SavingTransactionId.CreateUnique(),
            Math.Abs(amount),
            TransactionType.Withdraw,
            dateTimeProvider.UtcNow);
    }
}
