namespace Ordering.Orders.Features.DeleteOrder;

public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{orderId}", async ([FromRoute] Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(orderId));
            var response = result.Adapt<DeleteOrderResponse>();
            return Results.Ok(response);
        });
    }
}
