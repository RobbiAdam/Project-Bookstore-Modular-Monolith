namespace Ordering.Orders.Models;
public class OrderItem : Entity<Guid>
{
    internal OrderItem(Guid orderId, Guid productId,string productName, int quantity, decimal price)
    {
        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public Guid OrderId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; } = default!;
    public decimal Price { get; set; } = default!;

}
