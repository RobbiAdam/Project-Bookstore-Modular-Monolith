using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;


public record GetProductsResponse(PaginatedResult<ProductDto> Products);
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var command = new GetProductsQuery(request);
            var result = await sender.Send(command);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        });
    }
}
