using DigitalWallet.Infrastructure.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalWallet.Infrastructure.Common.Extensions;

public static class MigrationHostExtensions
{
    public static async Task RunMigrations<TInitializer>(this IHost app)
        where TInitializer : IInitializer
    {
        await using var scope = app.Services.CreateAsyncScope();

        var services = scope.ServiceProvider;

        var initializer = scope
            .ServiceProvider
            .GetRequiredService<TInitializer>();

        await initializer.InitializeAsync();
    }
}
