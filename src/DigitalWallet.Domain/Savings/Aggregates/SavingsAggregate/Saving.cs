using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Enums;
using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Common.Primitives;
using DigitalWallet.Domain.Common.ValueObjects;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.Entities;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;

namespace DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;

public sealed class Saving : AggregateRoot<SavingId>
{
    private readonly List<SavingTransaction> _transactions = new();

    #region Ctor for migrations
    public Saving(SavingId id) : base(id)
    {
        //For migrations only
    }
    #endregion

    private Saving(
        SavingId id,
        decimal balance,
        CurrencyCode currency,
        UserId userId,
        DateTime created)
        : base(id)
    {
        Balance = balance;
        Currency = currency;
        UserId = userId;
        Created = created;
    }

    public decimal Balance { get; private set; }
    public CurrencyCode Currency { get; private set; }
    public UserId UserId { get; private set; }

    public IReadOnlyCollection<SavingTransaction> Transactions => _transactions.AsReadOnly();

    public DateTime Created { get; private set; }
    public DateTime? Modified { get; private set; }

    public static Saving CreateNew(
        CurrencyCode currency,
        UserId userId,
        IDateTimeProvider dateTimeProvider)
    {
        return new(
            SavingId.CreateUnique(),
            decimal.Zero,
            currency,
            userId,
            dateTimeProvider.UtcNow);
    }

    public void CreateTransaction(
        decimal amount,
        TransactionType? type,
        IDateTimeProvider dateTimeProvider)
    {
        if (type == TransactionType.Deposit)
            DepositMoney(amount, dateTimeProvider);

        if (type == TransactionType.Withdraw)
            WithdrawMoney(amount, dateTimeProvider);
    }

    private void DepositMoney(
        decimal amount,
        IDateTimeProvider dateTimeProvider)
    {
        Balance += Math.Abs(amount);
        Modified = dateTimeProvider.UtcNow;

        _transactions
            .Add(SavingTransaction
                    .CreateNewDeposit(
                        amount,
                        dateTimeProvider));
    }

    private void WithdrawMoney(
        decimal amount,
        IDateTimeProvider dateTimeProvider)
    {
        Balance -= Math.Abs(amount);
        Modified = dateTimeProvider.UtcNow;

        _transactions
            .Add(SavingTransaction
                .CreateNewWithdraw(
                    amount,
                    dateTimeProvider));
    }
}
