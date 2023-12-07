using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Enums;
using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Common.ValueObjects;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using FluentAssertions;
using NSubstitute;

namespace DigitalWallet.Domain.UnitTests.Savings.Aggregates.SavingAggregate;

public sealed class SavingTests
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public SavingTests()
    {
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _dateTimeProvider.UtcNow.Returns(new DateTime(2023, 11, 1));
    }

    [Fact]
    public void CreateNew_ShouldReturn_NewSaving()
    {
        // Arrange
        var userId = UserId.CreateUnique();

        // Act
        var saving = Saving.CreateNew(
            CurrencyCode.PLN,
            userId,
            _dateTimeProvider);

        // Assert
        saving.Should().NotBeNull();
        saving.UserId.Should().Be(userId);
        saving.Balance.Should().Be(0);
        saving.Currency.Should().Be(CurrencyCode.PLN);
    }

    [Fact]
    public void CreateTransaction_ShouldReturn_IncreasedBalance()
    {
        // Arrange
        decimal amount = 1000;
        var dateTimeProvioderModified = _dateTimeProvider;
        var saving = Saving.CreateNew(
            CurrencyCode.PLN,
            UserId.CreateUnique(),
            _dateTimeProvider);
        var transactionType = TransactionType.GetTransactionType("Deposit");
        DateTime modified = new DateTime(2023, 11, 20);
        dateTimeProvioderModified.UtcNow.Returns(modified);

        // Act
        saving.CreateTransaction(
            amount,
            transactionType,
            dateTimeProvioderModified);

        // Assert
        saving.Balance.Should().Be(amount);
        saving.Transactions.Should().HaveCount(1);
        saving.Modified.Should().Be(modified);
    }

    [Fact]
    public void CreateTransaction_ShouldReturn_DecreasedBalance()
    {
        // Arrange
        decimal depositAmount = 1000;
        decimal withdrawiAmount = 500;
        var dateTimeProviderModified = _dateTimeProvider;
        DateTime modified = new DateTime(2023, 11, 20);
        var transactionType = TransactionType.GetTransactionType("Withdraw");
        dateTimeProviderModified.UtcNow.Returns(modified);
        var saving = Saving.CreateNew(
            CurrencyCode.PLN,
            UserId.CreateUnique(),
            _dateTimeProvider);

        saving.CreateTransaction(
            depositAmount,
            TransactionType.Deposit,
            _dateTimeProvider);

        // Act
        saving.CreateTransaction(
            withdrawiAmount,
            transactionType,
            dateTimeProviderModified);

        // Assert
        saving.Balance.Should().Be(depositAmount - withdrawiAmount);
        saving.Transactions.Should().HaveCount(2);
        saving.Modified.Should().Be(modified);
    }
}
