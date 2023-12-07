using DigitalWallet.Application.Savings.Repositories;
using DigitalWallet.Application.Savings.Repositories.Commands;
using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Domain.Common.Shared;
using DigitalWallet.Domain.Common.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalWallet.Application.Savings.Transactions;

internal sealed class TransactionCommandHandler
    : IRequestHandler<TransactionCommand, Result>
{
    private readonly ILogger<TransactionCommandHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ISavingCommandRepository _savingCommandRepository;
    private readonly ISavingQueryRepository _savingQueryRepository;

    public TransactionCommandHandler(
        ILogger<TransactionCommandHandler> logger,
        IDateTimeProvider dateTimeProvider,
        ISavingCommandRepository savingCommandRepository,
        ISavingQueryRepository savingQueryRepository)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
        _savingCommandRepository = savingCommandRepository;
        _savingQueryRepository = savingQueryRepository;
    }

    public async Task<Result> Handle(
        TransactionCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var saving = await _savingQueryRepository
                .GetSavingByIdAsync(
                    request.SavingId,
                    cancellationToken);

            saving.CreateTransaction(
                request.Amount,
                TransactionType.GetTransactionType(request.Type),
                _dateTimeProvider);

            await _savingCommandRepository
                .UpdateSavingAsync(
                    saving,
                    cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure(Error.InvalidTransaction);
        }
    }
}
