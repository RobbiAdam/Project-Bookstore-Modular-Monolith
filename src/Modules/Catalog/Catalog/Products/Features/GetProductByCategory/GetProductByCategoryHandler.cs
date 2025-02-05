namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);

public class GetProductByCategoryHandler(CatalogDbContext context) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Where(p => p.Category.Contains(query.Category))
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductByCategoryResult(productDtos);
    }
}
