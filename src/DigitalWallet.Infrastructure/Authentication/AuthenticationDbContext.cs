using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.Entities;
using DigitalWallet.Infrastructure.Authentication.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Infrastructure.Authentication;

public sealed class AuthenticationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly string _schema = "Authentication";

    public AuthenticationDbContext(
        DbContextOptions<AuthenticationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(_schema);

        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new OutboxConfiguration());

        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
