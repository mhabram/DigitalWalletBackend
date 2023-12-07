using DigitalWallet.Domain.Common.Shared;
using MediatR;

namespace DigitalWallet.Application.Authentication.ConfirmEmails;

public sealed record ConfirmEmailCommand(string Token)
    : IRequest<Result>;
