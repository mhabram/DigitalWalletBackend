using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.Authentication.Covnerters;

internal sealed class UserIdJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(UserId);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var guid = serializer.Deserialize<Guid>(reader);
        return UserId.Create(guid);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is not null)
        {
            var id = (UserId)value;
            serializer.Serialize(writer, id.Value);
        }
    }
}
