using Carter;
using DigitalWallet.Application.Authentication.ConfirmEmails;
using DigitalWallet.Application.Authentication.LogInQueries;
using DigitalWallet.Application.Authentication.UserRegistrations;
using DigitalWallet.Contracts.Authentication;
using MediatR;

namespace DigitalWallet.Api.Endpoints.Authentication;

public sealed class AuthenticationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("Api/Authentication");

        group.MapPost("/Registration", RegisterNewUser);
        group.MapPost("/Login", LogIn);
        group.MapGet("/{token}/Confirm", ConfirmEmail); // TODO change for PATCH while front will be working

    }

    private static async Task<IResult> LogIn(
        LogInRequest request,
        ISender sender)
    {
        var query = new LogInQuery(
            request.Login,
            request.Password);

        var result = await sender.Send(query);

        return result.IsSuccess
            ? Results.Ok(
                new AuthenticationResponse(
                    result.Value.Token))
            : Results.NotFound(result.Error);
    }

    private static async Task<IResult> RegisterNewUser(
        RegisterNewUserRequest request,
        ISender sender)
    {
        var command = new RegisterNewUserCommand(
            request.Login,
            request.Email,
            request.Password,
            request.PhoneNumber);
        
        var result = await sender.Send(command);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> ConfirmEmail(
        string token,
        ISender sender)
    {
        var result = await sender.Send(new ConfirmEmailCommand(token));

        return result.IsSuccess
            ? Results.Ok()
            : Results.BadRequest(result.Error);
    }
}
