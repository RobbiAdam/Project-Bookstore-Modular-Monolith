namespace Ordering.Orders.Dtos;
public record QueryOrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    List<OrderItemDto> OrderItems,
    decimal TotalPrice
    );