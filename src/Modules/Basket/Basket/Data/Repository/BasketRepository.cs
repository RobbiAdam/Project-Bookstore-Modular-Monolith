namespace Basket.Data.Repository;
public class BasketRepository(
    BasketDbContext context) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string username, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var query = context.ShoppingCarts
            .Include(x => x.Items)
            .Where(x => x.Username == username);

        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        var basket = await query.SingleOrDefaultAsync(cancellationToken);

        return basket ?? throw new BasketNotFoundException(username);
    }
    public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        context.ShoppingCarts.Add(cart);
        await context.SaveChangesAsync(cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        var basket = await GetBasket(username, false, cancellationToken);
        context.ShoppingCarts.Remove(basket);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<int> SaveChangesAsync(string? username = null, CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}
