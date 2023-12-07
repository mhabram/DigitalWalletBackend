namespace DigitalWallet.Contracts.Savings;

public sealed record WithdrawRequest(
    Guid SavingId,
    decimal Amount);
