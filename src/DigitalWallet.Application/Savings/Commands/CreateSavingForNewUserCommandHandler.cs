using DigitalWallet.Application.Savings.Repositories.Commands;
using DigitalWallet.Domain.Common.Enums;
using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Savings.Commands;

internal sealed class CreateSavingForNewUserCommandHandler
    : IRequestHandler<CreateSavingForNewUserCommand>
{
    private readonly ILogger<CreateSavingForNewUserCommandHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISavingCommandRepository _savingCommandRepository;

    public CreateSavingForNewUserCommandHandler(
        ILogger<CreateSavingForNewUserCommandHandler> logger,
        IDateTimeProvider dateTimeProvider,
        ISavingCommandRepository savingCommandRepository)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
        _savingCommandRepository = savingCommandRepository;
    }

    public async Task Handle(
        CreateSavingForNewUserCommand request,
        CancellationToken cancellationToken)
    {
        var saving = Saving.CreateNew(
            CurrencyCode.EUR,
            request.userId,
            _dateTimeProvider);

        try
        {
            await _savingCommandRepository
                .CreateNewSavingAsync(
                    saving,
                    cancellationToken);
            _logger.LogInformation("The saving account has been initialized to the new user account.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}
