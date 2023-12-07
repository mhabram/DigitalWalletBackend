namespace DigitalWallet.Contracts.Authentication;

public sealed record LogInRequest(
    string Login,
    string Password);
