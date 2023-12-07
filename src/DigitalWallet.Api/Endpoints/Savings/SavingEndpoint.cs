using Carter;
using DigitalWallet.Application.Savings.Transactions;
using DigitalWallet.Contracts.Savings.Transactions;
using DigitalWallet.Domain.Savings.Aggregates.SavingsAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Api.Endpoints.Savings;

public sealed class SavingEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Api/Savings");

        group.MapPost("/Transactions", Transactions);
    }

    private static async Task<IResult> Transactions(
        TransactionRequest request,
        [FromQuery] string type,
        ISender sender)
    {
        var command = new TransactionCommand(
            SavingId.Create(request.SavingId),
            request.Amount,
            type);

        var result = await sender.Send(command);

        return result.IsSuccess
            ? Results.Ok()
            : Results.BadRequest(result.Error);
    }
}
