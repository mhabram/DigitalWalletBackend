using DigitalWallet.Application.Common.Behaviors;
using DigitalWallet.Application.Common.Email;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace DigitalWallet.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        services.AddScoped<IEmailService, EmailService>();

        var emailSmtpSettings = configuration
            .GetSection(nameof(EmailSmtpSettings))
            .Get<EmailSmtpSettings>()!;
        services.AddSingleton(Options.Create(emailSmtpSettings));


        return services;
    }
}
