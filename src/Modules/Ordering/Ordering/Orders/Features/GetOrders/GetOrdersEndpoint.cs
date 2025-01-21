using Shared.Pagination;

namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrdersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));

            var response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(response);

        });
    }
}
