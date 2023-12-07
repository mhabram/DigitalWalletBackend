using DigitalWallet.Domain.Common.Shared;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using MediatR;

namespace DigitalWallet.Application.Savings.Transactions;

public sealed record TransactionCommand(
    SavingId SavingId,
    decimal Amount,
    string Type)
    : IRequest<Result>;
