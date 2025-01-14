using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.GetProductById;

//public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
//public record GetProductByIdResult(ProductDto Product);
public class GetProductByIdQueryHandler(CatalogDbContext context) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        var productDto = product.Adapt<ProductDto>();

        return new GetProductByIdResult(productDto);

    }
}
