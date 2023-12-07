using DigitalWallet.Domain.Authentication.Covnerters;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;

[JsonConverter(typeof(UserIdJsonConverter))]
public readonly record struct UserId
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() => new(Guid.NewGuid());
    public static UserId Create(Guid value) => new(value);
}
