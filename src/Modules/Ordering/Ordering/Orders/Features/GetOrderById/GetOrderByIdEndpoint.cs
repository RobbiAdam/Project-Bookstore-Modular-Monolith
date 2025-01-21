namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdResponse(OrderDto Order);

public class GetOrderByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderId}", async ([FromRoute] Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdQuery(orderId));
            var response = result.Adapt<GetOrderByIdResponse>();
            return Results.Ok(response);
        });
    }
}
