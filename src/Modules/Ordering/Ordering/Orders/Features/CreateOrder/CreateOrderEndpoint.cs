namespace Ordering.Orders.Features.CreateOrder;
public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid OrderId);
public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"/orders/{response.OrderId}", response);
        });
    }
}
