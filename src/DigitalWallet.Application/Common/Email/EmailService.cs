using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace DigitalWallet.Application.Common.Email;

public sealed class EmailService : IEmailService
{
    private readonly EmailSmtpSettings _emailSmtpSettings;

    public EmailService(IOptions<EmailSmtpSettings> emailSmtpSettings)
    {
        _emailSmtpSettings = emailSmtpSettings.Value;
    }

    public async Task SendEmailAsync(EmailMessage request)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailSmtpSettings.Email));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _emailSmtpSettings.Host,
            _emailSmtpSettings.Port,
            SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_emailSmtpSettings.Email, _emailSmtpSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
