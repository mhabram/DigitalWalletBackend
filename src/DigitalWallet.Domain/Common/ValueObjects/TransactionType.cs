namespace DigitalWallet.Domain.Common.ValueObjects;

public sealed record TransactionType(string Value)
{
    public static TransactionType Deposit => new(nameof(Deposit));
    public static TransactionType Withdraw => new(nameof(Withdraw));

    public static TransactionType? GetTransactionType(string value)
    {
        if (nameof(Deposit)
            .Equals(value.ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase))
        {
            return new(nameof(Deposit));
        }

        if (nameof(Withdraw)
            .Equals(value.ToUpperInvariant(), StringComparison.InvariantCultureIgnoreCase))
        {
            return new(nameof(Withdraw));
        }

        return null;
    }
}
