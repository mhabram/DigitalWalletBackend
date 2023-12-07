using DigitalWallet.Domain.Common.Interfaces;
using DigitalWallet.Infrastructure.Authentication;
using DigitalWallet.Infrastructure.BackgroudJobs;
using DigitalWallet.Infrastructure.Common.Interceptors;
using DigitalWallet.Infrastructure.Common.Services;
using DigitalWallet.Infrastructure.Savings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalWallet.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApplicationAuthentication(configuration)
            .AddApplicationSavings(configuration)
            .AddBackgroundJobs();
        
        services.AddSingleton<ConvertDomainEventsToOutboxMessageInterceptor>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
}
