using DigitalWallet.Domain.Common.Primitives;

namespace DigitalWallet.Domain.Common.Exceptions;

public sealed class BusinessRuleValidationException : Exception
{
    public IDomainRule BrokenRule { get; }
    public string Details { get; }

    public BusinessRuleValidationException(IDomainRule brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        Details = brokenRule.Message;
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }
}
