namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketRequest(ShoppingCartDto Cart);
public record CreateBasketResponse(Guid Id);

internal class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (CreateBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateBasketResponse>();
            return Results.Created($"/baskets/{response.Id}", response);
        })
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create Basket");
    }
}
