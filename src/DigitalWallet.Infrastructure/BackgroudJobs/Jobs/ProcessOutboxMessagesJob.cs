using DigitalWallet.Domain.Common.Outbox;
using DigitalWallet.Domain.Common.Primitives;
using DigitalWallet.Infrastructure.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace DigitalWallet.Infrastructure.BackgroudJobs.Jobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly AuthenticationDbContext _context;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(
        AuthenticationDbContext context,
        IPublisher publisher,
        ILogger<ProcessOutboxMessagesJob> logger)
    {
        _context = context;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _context
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                _logger.LogError($"Outbox message domain event is null for id: {outboxMessage.Id}");
                continue;
            }

            try
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedAt(DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                outboxMessage.CreateErrorMessage(ex.Message);
            }
        }

        await _context.SaveChangesAsync();
    }
}
