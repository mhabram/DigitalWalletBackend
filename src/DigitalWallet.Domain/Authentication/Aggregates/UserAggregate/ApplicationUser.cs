using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Authentication.DomainEvents;
using DigitalWallet.Domain.Common.Primitives;
using Microsoft.AspNetCore.Identity;

namespace DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;

public class ApplicationUser : IdentityUser<Guid>, IDomainEventEntityBase
{
    private readonly List<IDomainEvent> _domainEvents = new();

    // For migrations only
    public ApplicationUser()
    { }

    private ApplicationUser(
        Guid id,
        string userName,
        string email,
        string? phoneNumber)
    {
        Id = id;
        UserName = userName;
        NormalizedUserName = userName.ToUpper();
        Email = email;
        NormalizedEmail = email.ToUpper();
        PhoneNumber = phoneNumber;
    }

    public virtual ICollection<IdentityUserClaim<Guid>>? Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<Guid>>? Logins { get; set; }
    public virtual ICollection<IdentityUserToken<Guid>>? Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<Guid>>? UserRoles { get; set; }

    public static ApplicationUser CreateNew(
        string userName,
        string email,
        string? phoneNumber = "")
    {
        var user = new ApplicationUser(
            Guid.NewGuid(),
            userName,
            email,
            phoneNumber);

        user.RaiseDomainEvent(
            new NewUserRegisteredDomainEvent(
                UserId.Create(
                    user.Id)));

        return user;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<IDomainEvent> GetDomainEevents()
    {
        return _domainEvents.AsReadOnly();
    }

    public void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
