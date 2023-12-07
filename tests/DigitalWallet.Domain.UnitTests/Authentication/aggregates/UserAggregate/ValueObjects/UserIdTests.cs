using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using FluentAssertions;

namespace DigitalWallet.Domain.UnitTests.Authentication.aggregates.UserAggregate.ValueObjects;

public sealed class UserIdTests
{
    [Fact]
    public void SameValues_ShouldReturn_Equal()
    {
        var id = Guid.NewGuid();
        var userId1 = UserId.Create(id);
        var userId2 = UserId.Create(id);

        userId1.Should().BeEquivalentTo(userId2);
    }

    [Fact]
    public void DifferentValues_ShouldReturn_Unequal()
    {
        var userId1 = UserId.CreateUnique();
        var userId2 = UserId.CreateUnique();

        userId1.Should().NotBeEquivalentTo(userId2);
    }
}
