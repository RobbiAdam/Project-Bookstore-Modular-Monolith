namespace Basket.Basket.Features.AddItemToBasket;
public record AddItemToBasketRequest(string Username, ShoppingCartItemDto Items);
public record AddItemToBasketResponse(Guid BasketId);
public class AddItemToBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets/{username}/items", async (
            [FromRoute] string username, [FromBody] AddItemToBasketRequest request, ISender sender) =>
        {
            var command = new AddItemToBasketCommand(username, request.Items);
            var result = await sender.Send(command);
            var response = result.Adapt<AddItemToBasketResponse>();
            return Results.Created($"/baskets/{response.BasketId}", response.BasketId);
        })
        .Produces<AddItemToBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Add Item Into Basket")
        .WithDescription("Add Item Into Basket");        
    }
}
