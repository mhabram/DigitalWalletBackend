using Carter;
using DigitalWallet.Api;
using DigitalWallet.Application;
using DigitalWallet.Infrastructure;
using DigitalWallet.Infrastructure.Authentication;
using DigitalWallet.Infrastructure.Common.Extensions;
using DigitalWallet.Infrastructure.Savings;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapCarter();

await app.RunMigrations<AuthenticationDbContextInitializer>();
await app.RunMigrations<SavingDbContextInitializer>();

await app.RunAsync();
