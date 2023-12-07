using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common;
using DigitalWallet.Domain.Common.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Authentication.UserRegistrations;

internal sealed class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand, Result<UserId>>
{
    private readonly ILogger<RegisterNewUserCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterNewUserCommandHandler(
        ILogger<RegisterNewUserCommandHandler> logger,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<Result<UserId>> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
    {
        var user = ApplicationUser.CreateNew(
            request.Login,
            request.Email,
            request.PhoneNumber);

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Roles.Member);
            _logger.LogInformation("User created a new account with password.");

            return Result.Success(UserId.Create(user.Id));
        }

        return Result.Failure<UserId>(Error.CouldNotRegisterTheUser);
    }
}
