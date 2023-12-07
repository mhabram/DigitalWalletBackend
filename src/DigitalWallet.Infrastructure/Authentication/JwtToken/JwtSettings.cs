namespace DigitalWallet.Infrastructure.Authentication.JwtToken;

public sealed record JwtSettings(
    int ExpiryMinutes,
    string Secret,
    string Issuer,
    string Audience);
