namespace DigitalWallet.Contracts.Savings.Transactions;

public sealed record TransactionRequest(
    Guid SavingId,
    decimal Amount);
