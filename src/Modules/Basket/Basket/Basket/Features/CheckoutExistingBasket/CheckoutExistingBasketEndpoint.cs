namespace Basket.Basket.Features.CheckoutExistingBasket;

public record CheckoutExistingBasketRequest(BasketCheckout BasketCheckout);

public record CheckoutExistingBasketResponse(bool IsSuccess);

internal class CheckoutExistingBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/checkout/{username}",
            async ([FromRoute] string username, [FromBody] CheckoutExistingBasketRequest request, ISender sender) =>
        {
            var command = new CheckoutExistingBasketCommand(username, request.BasketCheckout);

            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutExistingBasketResponse>();
            return Results.Ok(response);
        });
    }
}
