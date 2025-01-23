using System.Security.Claims;
using Basket.Basket.Features.GetBasketByUsername;

namespace Basket.Basket.Features.GetCurrentUserBasket;

public record GetCurrentUserBasketResponse(ShoppingCartDto Cart);
public class GetCurrentUserBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/me", async (ClaimsPrincipal user, ISender sender) =>
        {
            var username = user.Identity!.Name!;
            var query = new GetBasketByUsernameQuery(username);
            var response = await sender.Send(query);
            var result = response.Adapt<GetBasketByUsernameResponse>();
            return Results.Ok(result);
        })
        .Produces<GetCurrentUserBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket")
        .RequireAuthorization();
    }
}
