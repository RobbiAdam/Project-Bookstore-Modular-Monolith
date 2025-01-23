namespace Basket.Basket.Features.GetBasketByUsername;

public record GetBasketByUsernameResponse(ShoppingCartDto Cart);
public class GetBasketByUsernameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/{username}", async ([FromRoute] string username, ISender sender) =>
        {
            var query = new GetBasketByUsernameQuery(username);
            var response = await sender.Send(query);
            var result = response.Adapt<GetBasketByUsernameResponse>();
            return Results.Ok(result);
        })
        .Produces<GetBasketByUsernameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket");
    }
}
