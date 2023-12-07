namespace DigitalWallet.Contracts.Savings;

public sealed record DepositRequest(
    Guid SavingId,
    decimal Amount);
