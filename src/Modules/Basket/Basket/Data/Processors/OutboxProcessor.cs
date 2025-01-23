using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Basket.Data.Processors;
public class OutboxProcessor(
    IServiceProvider serviceProvider, IBus bus, ILogger<OutboxProcessor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<BasketDbContext>();
                var outboxMessages = await context.OutboxMessages
                    .Where(m => m.ProcessedOn == null)
                    .ToListAsync(stoppingToken);

                foreach (var message in outboxMessages)
                {
                    var eventType = Type.GetType(message.Type);
                    if (eventType == null)
                    {
                        logger.LogWarning("Could not resolve event type : {eventType}", message.Type);
                        continue;
                    }

                    var eventMessage = JsonSerializer.Deserialize(message.Content, eventType);
                    if (eventMessage == null)
                    {
                        logger.LogWarning("Could not deserialize event message : {message}", message.Content);
                        continue;
                    }

                    await bus.Publish(eventMessage, stoppingToken);

                    message.ProcessedOn = DateTime.UtcNow;

                    logger.LogInformation("Successfully processed outbox message with Id : {id}", message.Id);

                }
                await context.SaveChangesAsync(stoppingToken);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while processing outbox");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
