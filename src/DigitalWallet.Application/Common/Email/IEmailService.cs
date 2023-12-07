namespace DigitalWallet.Application.Common.Email;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage request);
}
