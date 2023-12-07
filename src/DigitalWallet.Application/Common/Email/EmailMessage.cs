namespace DigitalWallet.Application.Common.Email;

public record struct EmailMessage(
    string To,
    string Subject,
    string Body);
