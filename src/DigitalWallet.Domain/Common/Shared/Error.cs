namespace DigitalWallet.Domain.Common.Shared;

public record Error(string Code, string Message)
{

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.");

    #region Authentication
    public static readonly Error CouldNotRegisterTheUser = new("Error.CouldNotRegisterTheUser", "The user could not be registered.");
    public static readonly Error InvalidToken = new("Error.InvalidToken", "The provided token is invalid.");
    public static readonly Error LockedAccount = new("Error.Locked", "The account is locked.");
    #endregion

    #region Savings
    public static readonly Error InvalidTransaction = new("Error.InvalidTransaction", "Incorrect transaction");
    #endregion
}
