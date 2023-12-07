using Bogus;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using DigitalWallet.Domain.Authentication.DomainEvents;
using FluentAssertions;

namespace DigitalWallet.Domain.UnitTests.Authentication.aggregates.UserAggregate;

public sealed class ApplicationUserTests
{
    [Fact]
    public void CreateNew_ShouldReturn_NewApplicationUser()
    {
        // Arrange
        var person = new Person();

        // Act
        var user = ApplicationUser.CreateNew(
            person.UserName,
            person.Email,
            person.Phone);

        // Assert
        user.Should().NotBeNull();
        user.UserName.Should().Be(person.UserName);
        user.Email.Should().Be(person.Email);
        user.PhoneNumber.Should().Be(person.Phone);
        user.GetDomainEevents()[0].Should().BeOfType<NewUserRegisteredDomainEvent>();
    }

    [Fact]
    public void ClearDomainEvent_Should_ClearList()
    {
        // Arrange
        var person = new Person();
        var user = ApplicationUser.CreateNew(
            person.UserName,
            person.Email,
            person.Phone);

        // Act
        user.ClearDomainEvents();

        // Assert
        user.GetDomainEevents().Should().HaveCount(0);
    }
}
