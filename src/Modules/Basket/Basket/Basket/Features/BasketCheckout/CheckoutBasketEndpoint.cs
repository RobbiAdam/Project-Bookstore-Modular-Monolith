namespace Basket.Basket.Features.BasketCheckout;

public record CheckoutBasketRequest(BasketCheckoutDto BasketCheckout);

public record CheckoutBasketResponse(bool IsSuccess);

public class CheckoutBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("baskets/checkout", async (CheckoutBasketRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await mediator.Send(command);
            var response = result.Adapt<CheckoutBasketResponse>();
            return Results.Ok(response);
        });
    }
}

