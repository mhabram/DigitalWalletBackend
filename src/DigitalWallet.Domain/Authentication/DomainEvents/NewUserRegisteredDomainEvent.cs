using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.ValueObjects;
using DigitalWallet.Domain.Common.Primitives;

namespace DigitalWallet.Domain.Authentication.DomainEvents;

public sealed record NewUserRegisteredDomainEvent(UserId Id)
    : IDomainEvent;
