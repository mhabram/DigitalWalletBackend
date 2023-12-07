using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWallet.Infrastructure.Authentication.Configuration;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasMany(e => e.Claims)
            .WithOne()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        builder.HasMany(e => e.Logins)
            .WithOne()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        builder.HasMany(e => e.Tokens)
            .WithOne()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}
