namespace DigitalWallet.Application.Authentication.LogInQueries;

internal class LockoutOnFailure
{
    private readonly bool _lockoutOnFailure;

    private LockoutOnFailure(bool lockoutOnFailure) =>
        _lockoutOnFailure = lockoutOnFailure;

    public static LockoutOnFailure True => new(true);

    public static LockoutOnFailure False => new(false);

    public static implicit operator bool(LockoutOnFailure lockoutOnFailure) =>
        lockoutOnFailure._lockoutOnFailure;

    public static explicit operator LockoutOnFailure(bool lockoutOnFailure) =>
        new(lockoutOnFailure);
}
