using DigitalWallet.Domain.Common.Interfaces;

namespace DigitalWallet.Infrastructure.Common.Services;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
