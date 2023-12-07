using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Shared;
using MediatR;

namespace DigitalWallet.Application.Authentication.UserRegistrations;

public record RegisterNewUserCommand(
    string Login,
    string Email,
    string Password,
    string? PhoneNumber)
    : IRequest<Result<UserId>>;
