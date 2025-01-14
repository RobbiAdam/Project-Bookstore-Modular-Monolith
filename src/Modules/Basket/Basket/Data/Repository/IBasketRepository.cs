namespace Basket.Data.Repository;
public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username, bool asNoTracking = true, CancellationToken cancellationToken = default);
    Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(string? username = null,CancellationToken cancellationToken = default);

}
