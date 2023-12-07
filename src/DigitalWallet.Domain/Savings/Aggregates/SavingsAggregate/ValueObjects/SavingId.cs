using DigitalWallet.Domain.Savings.Converters;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;

[JsonConverter(typeof(SavingIdJsonConverter))]
public readonly record struct SavingId
{
    public Guid Value { get; }

    private SavingId(Guid value)
    {
        Value = value;
    }

    public static SavingId CreateUnique() => new(Guid.NewGuid());
    public static SavingId Create(Guid value) => new(value);
}
