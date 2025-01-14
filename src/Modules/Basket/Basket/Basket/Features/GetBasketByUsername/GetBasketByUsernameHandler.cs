namespace Basket.Basket.Features.GetBasketByUsername;

public record GetBasketByUsernameQuery(string Username) : IQuery<GetBasketByUsernameResult>;

public record GetBasketByUsernameResult(ShoppingCartDto Cart);

public class GetBasketByUsernameQueryHandler(
    IBasketRepository repository) : IQueryHandler<GetBasketByUsernameQuery, GetBasketByUsernameResult>
{
    public async Task<GetBasketByUsernameResult> Handle(GetBasketByUsernameQuery query, CancellationToken cancellationToken)
    {

        var cart = await repository.GetBasket(query.Username, true, cancellationToken);

        var cartDto = cart.Adapt<ShoppingCartDto>();

        return new GetBasketByUsernameResult(cartDto);
    }
}
