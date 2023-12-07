using DigitalWallet.Application.Authentication.Interfaces;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Common.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Authentication.LogInQueries;

internal sealed class LogInQueryHandler : IRequestHandler<LogInQuery, Result<AuthenticationResult>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<LogInQueryHandler> _logger;

    public LogInQueryHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<LogInQueryHandler> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<Result<AuthenticationResult>> Handle(LogInQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager
            .FindByNameAsync(request.Login);

        if (user is null)
        {
            _logger.LogInformation("Invalid login attempt.");
            return Result.Failure<AuthenticationResult>(Error.NullValue);
        }

        var result = await _signInManager
            .PasswordSignInAsync(
                user,
                request.Password,
                request.RememberMe,
                LockoutOnFailure.True);

        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            var token = _jwtTokenGenerator
                .GenerateToken(user);

            return Result.Success(new AuthenticationResult(token));
        }

        if (result.IsLockedOut)
        {
            _logger.LogInformation("User account locked out.");
            return Result.Failure<AuthenticationResult>(Error.LockedAccount);
        }
        
        _logger.LogInformation("Invalid login attempt.");
        return Result.Failure<AuthenticationResult>(Error.NullValue);
    }
}
