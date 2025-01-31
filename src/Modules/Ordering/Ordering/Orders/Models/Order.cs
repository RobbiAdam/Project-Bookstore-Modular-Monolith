namespace Ordering.Orders.Models;
public class Order : Aggregate<Guid>
{
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public Guid CustomerId { get; set; } = default!;
    public string OrderName { get; set; } = default!;
    public Address BillingAddress { get; set; } = default!;
    public Address ShippingAddress { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
    public decimal TotalPrice => _orderItems.Sum(x => x.Price * x.Quantity);

    public static Order Create(Guid Id, Guid CustomerId, string OrderName, Address BillingAddress, Address ShippingAddress, Payment Payment)
    {
        var order = new Order()
        {
            Id = Id,
            CustomerId = CustomerId,
            OrderName = OrderName,
            BillingAddress = BillingAddress,
            ShippingAddress = ShippingAddress,
            Payment = Payment,
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void AddOrderItem(Guid productId, string productName, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var existingItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var orderItem = new OrderItem(Id, productId, productName, quantity, price);
            _orderItems.Add(orderItem);
        }
    }

    public void RemoveOrderItem(Guid productId)
    {
        var existingItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            _orderItems.Remove(existingItem);
        }
    }
}
