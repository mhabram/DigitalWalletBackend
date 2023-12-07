using Carter;

namespace DigitalWallet.Api;

public static class ConfigureServices
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddCarter();

        return services;
    }
}
