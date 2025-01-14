namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketResponse(bool Success);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            var response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket");
    }
}
