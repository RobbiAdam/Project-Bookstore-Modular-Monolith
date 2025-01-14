using Microsoft.AspNetCore.Mvc;

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    string Description,
    List<string> Category,
    decimal Price);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (
            [FromRoute] Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = new UpdateProductCommand(
                new ProductDto(
                    id, request.Name, request.Description, request.Category, request.Price
                    ));


            var result = await sender.Send(command);
            var response = result.Adapt<UpdateProductResponse>();
            return Results.Ok(response);
        });
    }
}
