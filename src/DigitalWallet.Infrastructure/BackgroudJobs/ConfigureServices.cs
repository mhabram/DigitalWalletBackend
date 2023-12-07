using DigitalWallet.Infrastructure.BackgroudJobs.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace DigitalWallet.Infrastructure.BackgroudJobs;

public static class ConfigureServices
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
        });

        services.AddQuartzHostedService();

        return services;
    }
}
