using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using MediatR;

namespace DigitalWallet.Application.Savings.Commands;

public sealed record CreateSavingForNewUserCommand(
    UserId userId)
    : IRequest;
