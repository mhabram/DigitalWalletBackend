using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using FluentAssertions;

namespace DigitalWallet.Domain.UnitTests.Savings.Aggregates.SavingAggregate.ValueObjects;

public sealed class SavingDepositIdTests
{
    [Fact]
    public void SameValues_ShouldReturn_Equal()
    {
        var id = Guid.NewGuid();
        var savingDepositId1 = SavingTransactionId.Create(id);
        var savingDepositId2 = SavingTransactionId.Create(id);

        savingDepositId1.Should().BeEquivalentTo(savingDepositId2);
    }

    [Fact]
    public void DifferentValues_ShouldReturn_Unequal()
    {
        var savingDepositId1 = SavingTransactionId.CreateUnique();
        var savingDepositId2 = SavingTransactionId.CreateUnique();

        savingDepositId1.Should().NotBeEquivalentTo(savingDepositId2);
    }
}
