using MassTransit;
using Ordering.Orders.Features.CreateOrder;
using Shared.Messaging.Events;

namespace Ordering.Orders.EventsHandler;
public class BasketCheckoutIntegrationEventHandler(
    ISender sender, ILogger<BasketCheckoutIntegrationEventHandler> logger) : IConsumer<BasketCheckoutIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutIntegrationEvent> context)
    {
        logger.LogInformation("Integration event handled: {eventName}", context.Message.GetType().Name);
        var createOrderCommand = MapToCreateOrderCommand(context.Message);
        await sender.Send(createOrderCommand);

    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutIntegrationEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv, message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.Username,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            OrderItems: new List<OrderItemDto>()
        );
        return new CreateOrderCommand(orderDto);
    }
}
