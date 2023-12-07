using DigitalWallet.Domain.Savings.Converters;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;

[JsonConverter(typeof(SavingTransactionIdJsonConverter))]
public readonly record struct SavingTransactionId
{
    public Guid Value { get; }

    private SavingTransactionId(Guid value)
    {
        Value = value;
    }

    public static SavingTransactionId CreateUnique() => new(Guid.NewGuid());
    public static SavingTransactionId Create(Guid value) => new(value);
}
