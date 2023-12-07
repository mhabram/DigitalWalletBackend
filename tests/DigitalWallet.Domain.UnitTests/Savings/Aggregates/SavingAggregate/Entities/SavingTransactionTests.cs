using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Common.ValueObjects;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.Entities;
using FluentAssertions;
using NSubstitute;

namespace DigitalWallet.Domain.UnitTests.Savings.Aggregates.SavingAggregate.Entities;

public sealed class SavingTransactionTests
{
    private readonly DateTime _createDateTime;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SavingTransactionTests()
    {
        _createDateTime = new DateTime(2023, 11, 1);
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _dateTimeProvider.UtcNow.Returns(_createDateTime);
    }

    [Fact]
    public void CreateNewDeposit_ShouldReturn_NewDepositTransaction()
    {
        // Arrange
        decimal amount = 1000;

        // Act
        var transaction = SavingTransaction.CreateNewDeposit(
            amount,
            _dateTimeProvider);

        // Assert
        transaction.Amount.Should().Be(amount);
        transaction.DepositType.Should().Be(TransactionType.Deposit);
        transaction.Created.Should().Be(_createDateTime);
    }

    [Fact]
    public void CreateNewWithdraw_ShouldReturn_NewWithdrawTransaction()
    {
        // Arrange
        decimal amount = 1000;

        // Act
        var transaction = SavingTransaction.CreateNewWithdraw(
            amount,
            _dateTimeProvider);

        // Assert
        transaction.Amount.Should().Be(amount);
        transaction.DepositType.Should().Be(TransactionType.Withdraw);
        transaction.Created.Should().Be(_createDateTime);
    }
}
