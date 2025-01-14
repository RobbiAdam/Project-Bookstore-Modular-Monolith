namespace Basket.Basket.Models;
public class ShoppingCart : Aggregate<Guid>
{
    public string Username { get; private set; } = default!;

    private readonly List<ShoppingCartItem> _items = new();
    public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();

    public decimal TotalPrice => _items.Sum(x => x.Price * x.Quantity);

    public static ShoppingCart Create(Guid id, string username)
    {
        ArgumentException.ThrowIfNullOrEmpty(username);

        var shoppingCart = new ShoppingCart
        {
            Id = id,
            Username = username
        };

        return shoppingCart;
    }

    public void AddItem(Guid productId, int quantity, decimal price, string productName)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);
        ArgumentOutOfRangeException.ThrowIfNegative(price);

        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
        
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem(Id, productId, quantity,  price, productName);
            _items.Add(newItem);
        }
    }

    public void RemoveItem(Guid productId)
    {
        var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
        if (existingItem != null)
        {
            _items.Remove(existingItem);
        }
    }

}
