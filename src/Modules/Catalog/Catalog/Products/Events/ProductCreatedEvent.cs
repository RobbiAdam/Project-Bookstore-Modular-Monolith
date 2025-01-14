namespace Catalog.Products.Events;
public record class ProductCreatedEvent(Product Product) : IDomainEvent;
