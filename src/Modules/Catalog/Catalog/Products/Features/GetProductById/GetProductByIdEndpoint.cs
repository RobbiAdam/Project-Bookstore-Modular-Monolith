namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdResponse(ProductDto Product);
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var command = new GetProductByIdQuery(id);
            var result = await sender.Send(command);
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        });
    }
}
