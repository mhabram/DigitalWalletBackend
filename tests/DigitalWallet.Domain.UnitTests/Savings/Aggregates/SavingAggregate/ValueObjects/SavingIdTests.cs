using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using FluentAssertions;

namespace DigitalWallet.Domain.UnitTests.Savings.Aggregates.SavingAggregate.ValueObjects;

public sealed class SavingIdTests
{
    [Fact]
    public void SameValues_ShouldReturn_Equal()
    {
        var id = Guid.NewGuid();
        var savingId1 = SavingId.Create(id);
        var savingId2 = SavingId.Create(id);

        savingId1.Should().BeEquivalentTo(savingId2);
    }

    [Fact]
    public void DifferentValues_ShouldReturn_Unequal()
    {
        var savingId1 = SavingId.CreateUnique();
        var savingId2 = SavingId.CreateUnique();

        savingId1.Should().NotBeEquivalentTo(savingId2);
    }
}
