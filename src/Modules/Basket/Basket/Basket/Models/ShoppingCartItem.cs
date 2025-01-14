using System.Text.Json.Serialization;

namespace Basket.Basket.Models;
public class ShoppingCartItem : Entity<Guid>
{
    public Guid ShoppingCartId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public int Quantity { get; internal set; } = default!;

    public decimal Price { get; private set; } = default!;
    public string ProductName { get; private set; } = default!;


    internal ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, decimal price, string productName)
    {
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
    }

    [JsonConstructor]
    public ShoppingCartItem(Guid id, Guid shoppingCartId, Guid productId, int quantity, decimal price, string productName)
    {
        Id = id;
        ShoppingCartId = shoppingCartId;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
    }

    public void UpdatePrice(decimal newPrice)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newPrice);
        Price = newPrice;
    }
}
