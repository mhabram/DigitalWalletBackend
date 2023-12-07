using System.Text;
using DigitalWallet.Application.Savings.Commands;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Authentication.ConfirmEmails;

public sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ConfirmEmailCommandHandler> _logger;
    private readonly ISender _sender;

    public ConfirmEmailCommandHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ConfirmEmailCommandHandler> logger,
        ISender sender)
    {
        _userManager = userManager;
        _logger = logger;
        _sender = sender;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmationTokenWithUserId = Encoding
            .UTF8
            .GetString(Convert
                .FromBase64String(request.Token));

        var decodedToken = confirmationTokenWithUserId.Split(':');

        if (decodedToken.Length != 2)
        {
            _logger.LogError("Incorrect token.");
            return Result.Failure(Error.InvalidToken);
        }

        var confirmationToken = decodedToken[0];
        var userId = decodedToken[1];

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            _logger.LogError("User does not exist.");
            return Result.Failure(Error.NullValue);
        }

        if (await _userManager.IsEmailConfirmedAsync(user))
            return Result.Success();

        var result = await _userManager.ConfirmEmailAsync(user, confirmationToken);

        if (!result.Succeeded)
        {
            _logger.LogError("Unable to confirm the account");
            return Result.Failure(Error.InvalidToken);
        }

        await _sender.Send(new CreateSavingForNewUserCommand(
                UserId.Create(
                    user.Id)), cancellationToken);

        _logger.LogInformation("User confirmed the account.");
        return Result.Success();
    }
}
