using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using FluentAssertions;
using Newtonsoft.Json;

namespace DigitalWallet.Domain.UnitTests.Savings.Converters;

public sealed class SavingTransactionIdJsonConverterTests
{
    [Fact]
    public void StronglyTypedId_ShouldReturn_DirectValue()
    {
        // Arrange
        var id = SavingTransactionId.CreateUnique();

        // Act
        var content = JsonConvert.SerializeObject(id);
        Guid.TryParse(content.Replace("\"", string.Empty), out var result);

        // Assert
        result.Should().Be(id.Value);
    }

    [Fact]
    public void JsonStringToStronglyTypedId_ShouldReturn_GuidValue()
    {
        // Arrange
        var jsonString = "\"fe7e6b28-39e2-4068-a468-b690a429f3ea\"";

        // Act
        var result = JsonConvert.DeserializeObject<SavingTransactionId>(jsonString);

        // Assert
        result.Should().BeOfType<SavingTransactionId>();
    }
}
