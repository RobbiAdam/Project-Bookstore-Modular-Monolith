using Basket.Data.JsonConverters;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.Data.Repository;
public class CachedBasketRepository(
    IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new ShoppingCartConverter(), new ShoppingCartItemConverter() }
    };


    public async Task<ShoppingCart> GetBasket(string username, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        if (!asNoTracking)
        {
            return await repository.GetBasket(username, false, cancellationToken);
        }

        var cachedCart = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cachedCart))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedCart, _options)!;
        }

        var basket = await repository.GetBasket(username, asNoTracking, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(basket, _options), cancellationToken);
        return basket;

    }

    public async Task<ShoppingCart> CreateBasket(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await repository.CreateBasket(cart, cancellationToken);

        await cache.SetStringAsync(cart.Username, JsonSerializer.Serialize(cart, _options), cancellationToken);
        return cart;
    }

    public async Task<bool> DeleteBasket(string username, CancellationToken cancellationToken = default)
    {
        await repository.DeleteBasket(username, cancellationToken);
        await cache.RemoveAsync(username, cancellationToken);
        return true;
    }

    public async Task<int> SaveChangesAsync(string? username = null, CancellationToken cancellationToken = default)
    {
        var result = await repository.SaveChangesAsync(username, cancellationToken);

        if (username is not null)
        {
            await cache.RemoveAsync(username, cancellationToken);
        }

        return result;
    }
}
