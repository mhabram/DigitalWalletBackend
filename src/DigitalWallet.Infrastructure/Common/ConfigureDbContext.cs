using DigitalWallet.Infrastructure.Authentication;
using DigitalWallet.Infrastructure.Common.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalWallet.Infrastructure.Common;

public static class ConfigureDbContext
{
    public static void AddCustomDbContext<TContext>(
        this IServiceCollection services,
        string connectionString)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(
            (serviceProvider, options) =>
            {
                var interceptor = serviceProvider
                .GetService<ConvertDomainEventsToOutboxMessageInterceptor>();

                if (interceptor is null)
                    throw new ArgumentNullException(nameof(interceptor));

                options.UseNpgsql(
                    connectionString,
                    dbOptions => dbOptions
                        .MigrationsAssembly(typeof(AuthenticationDbContext)
                            .Assembly
                            .FullName))
                .AddInterceptors(interceptor);
            });
    }
}
