using DigitalWallet.Application.Savings.Repositories;
using DigitalWallet.Application.Savings.Repositories.Commands;
using DigitalWallet.Infrastructure.Common;
using DigitalWallet.Infrastructure.Savings.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalWallet.Infrastructure.Savings;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationSavings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<SavingDbContextInitializer>();

        services.AddScoped<ISavingCommandRepository, SavingCommandRepository>();
        services.AddScoped<ISavingQueryRepository, SavingQueryRepository>();

        services.AddCustomDbContext<SavingDbContext>(
            configuration.GetConnectionString("DefaultConnection")!);

        return services;
    }
}
