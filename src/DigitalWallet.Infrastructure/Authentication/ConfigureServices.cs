using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate.Entities;
using DigitalWallet.Domain.Authentication.Aggregates.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DigitalWallet.Application.Authentication.Interfaces;
using DigitalWallet.Infrastructure.Authentication.JwtToken;
using Microsoft.Extensions.Options;
using DigitalWallet.Infrastructure.Common;

namespace DigitalWallet.Infrastructure.Authentication;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        #region JwtToken
        var jwtSettings = configuration
            .GetSection(nameof(JwtSettings))
            .Get<JwtSettings>()!;
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        #endregion

        services.AddCustomDbContext<AuthenticationDbContext>(
            configuration.GetConnectionString("DefaultConnection")!);

        //services.AddDbContext<AuthenticationDbContext>(
        //    (serviceProvider, options) =>
        //    {
        //        var interceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessageInterceptor>();

        //        if (interceptor is null)
        //            throw new ArgumentNullException(nameof(interceptor));

        //        options.UseNpgsql(
        //            configuration.GetConnectionString("DefaultConnection"),
        //            dbOptions => dbOptions
        //                .MigrationsAssembly(typeof(AuthenticationDbContext)
        //                    .Assembly
        //                    .FullName))
        //            .AddInterceptors(interceptor);
        //    });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<AuthenticationDbContextInitializer>();
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Pasword settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // Sign in settings
            options.SignIn.RequireConfirmedEmail = true;
        });

        return services;
    }
}
