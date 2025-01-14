using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventsHandler;
public class ProductPriceChangedEventHandler(
    ILogger<ProductPriceChangedEventHandler> logger,
    IBus bus) : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

        var integrationEvents = new ProductPriceChangedIntegrationEvent
        {
            ProductId = notification.Product.Id,
            Name = notification.Product.Name,
            Category = notification.Product.Category,
            Description = notification.Product.Description,
            Price = notification.Product.Price
        }; 
        
        await bus.Publish(integrationEvents, cancellationToken);
    }
}
