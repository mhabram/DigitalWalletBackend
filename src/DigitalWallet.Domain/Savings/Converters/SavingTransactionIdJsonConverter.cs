using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.Savings.Converters;

internal sealed class SavingTransactionIdJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(SavingTransactionId);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var guid = serializer.Deserialize<Guid>(reader);
        return SavingTransactionId.Create(guid);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is not null)
        {
            var id = (SavingTransactionId)value;
            serializer.Serialize(writer, id.Value);
        }
    }
}
