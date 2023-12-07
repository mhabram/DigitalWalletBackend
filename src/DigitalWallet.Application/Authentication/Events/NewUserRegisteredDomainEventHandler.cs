using System.Text;
using DigitalWallet.Application.Common.Email;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.DomainEvents;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Authentication.Events;

internal sealed class NewUserRegisteredDomainEventHandler
    : INotificationHandler<NewUserRegisteredDomainEvent>
{
    private readonly ILogger<NewUserRegisteredDomainEventHandler> _logger;
    private readonly IEmailService _emailService;
    private readonly UserManager<ApplicationUser> _userManager;

    public NewUserRegisteredDomainEventHandler(
        ILogger<NewUserRegisteredDomainEventHandler> logger,
        IEmailService emailService,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _emailService = emailService;
        _userManager = userManager;
    }

    public async Task Handle(NewUserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.Id.Value.ToString());

        if (user is null)
            throw new NullReferenceException(nameof(user));

        var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user!);

        string tokenWithUserId = confirmationToken + ":" + user.Id.ToString();
        string encodedTokenWithUserId = Convert
            .ToBase64String(Encoding
                .UTF8
                .GetBytes(tokenWithUserId));

        string link = $"<a href='https://localhost:7246/Api/Authentication/{encodedTokenWithUserId}/Confirm'>link</a>";
        string subject = "Confirm your email";
        string confirmationEmailBody = $"Please confirm your account by clicking in the {link}.";

        EmailMessage emailRequest = new(
            user.Email!,
            subject,
            confirmationEmailBody);

        await _emailService.SendEmailAsync(emailRequest);

        _logger.LogInformation("Confirmation link to registration has been sent.");
    }
}
