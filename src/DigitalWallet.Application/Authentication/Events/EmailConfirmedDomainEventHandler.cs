//using DigitalWallet.Application.Savings.Repositories.Commands;
//using DigitalWallet.Domain.Common.Enums;
//using DigitalWallet.Domain.Common.Interfaces;
//using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace DigitalWallet.Application.Authentication.Events;

//internal sealed class EmailConfirmedDomainEventHandler
//    : INotificationHandler<NewUserEmailConfirmedDomainEvent>
//{
//    private readonly ILogger<EmailConfirmedDomainEventHandler> _logger;
//    private readonly IDateTimeProvider _dateTimeProvider;
//    private readonly ISavingCommandRepository _savingCommandRepository;

//    public EmailConfirmedDomainEventHandler(
//        ILogger<EmailConfirmedDomainEventHandler> logger,
//        IDateTimeProvider dateTimeProvider,
//        ISavingCommandRepository savingCommandRepository)
//    {
//        _logger = logger;
//        _dateTimeProvider = dateTimeProvider;
//        _savingCommandRepository = savingCommandRepository;
//    }

//    public async Task Handle(
//        NewUserEmailConfirmedDomainEvent notification,
//        CancellationToken cancellationToken)
//    {
//        var saving = Saving.CreateNew(
//            CurrencyCode.EUR,
//            notification.id,
//            _dateTimeProvider);

//        try
//        {
//            await _savingCommandRepository
//                .CreateNewSavingAsync(saving, cancellationToken);
//            _logger.LogInformation("The saving account has been initialized to the new user account.");
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, ex.Message);
//        }
//    }
//}
