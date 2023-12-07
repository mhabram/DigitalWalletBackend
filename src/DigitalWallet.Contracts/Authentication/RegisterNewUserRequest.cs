namespace DigitalWallet.Contracts.Authentication;

public sealed record RegisterNewUserRequest(
    string Login,
    string Email,
    string Password,
    string? PhoneNumber);
