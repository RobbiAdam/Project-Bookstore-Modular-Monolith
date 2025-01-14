namespace Catalog.Products.Models;
public class Product : Aggregate<Guid>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Category { get; set; } = default!;
    public decimal Price { get; set; }

    public static Product Create(Guid id, string name, string description, List<string> category, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product()
        {
            Id = id,
            Name = name,
            Description = description,
            Category = category,
            Price = price
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));

        return product;
    }

    public void Update(string name, string description, List<string> category, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        Name = name;
        Description = description;
        Category = category;

        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }

    }
}
