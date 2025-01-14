namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
        {
            var command = new GetProductByCategoryQuery(category);
            var result = await sender.Send(command);
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        });
    }
}
