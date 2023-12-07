using DigitalWallet.Domain.Common.Shared;
using MediatR;

namespace DigitalWallet.Application.Authentication.LogInQueries;

public sealed record LogInQuery(
    string Login,
    string Password,
    bool RememberMe = false)
    : IRequest<Result<AuthenticationResult>>;
