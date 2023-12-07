using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    // For migrations only
    public ApplicationRole()
    { }

    private ApplicationRole(string name)
    {
        Name = name;
        NormalizedName = name.ToUpperInvariant();
    }

    public static ApplicationRole CreateNew(string name) => new(name);
}
