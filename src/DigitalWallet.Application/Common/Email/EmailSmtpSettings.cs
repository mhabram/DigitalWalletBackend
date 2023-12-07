namespace DigitalWallet.Application.Common.Email;

public sealed record EmailSmtpSettings(
    string Host,
    int Port,
    string Email,
    string Password);
