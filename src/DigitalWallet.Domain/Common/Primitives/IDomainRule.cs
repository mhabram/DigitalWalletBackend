namespace DigitalWallet.Domain.Common.Primitives;

public interface IDomainRule
{
    bool IsBroken();
    string Message { get; }
}
