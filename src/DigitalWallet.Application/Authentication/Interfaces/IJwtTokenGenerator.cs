using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;

namespace DigitalWallet.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
