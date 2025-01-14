using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);
public class GetProductsQueryHandler(CatalogDbContext context) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await context.Products.LongCountAsync(cancellationToken);

        var products = await context.Products
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();

        return new GetProductsResult(new PaginatedResult<ProductDto>(
            pageIndex,
            pageSize,
            totalCount,
            productDtos));
    }

    //private List<ProductDto> MapToProductDto(List<Product> products)
    //{
    //    var productDtos = new List<ProductDto>();

    //    foreach (var product in products)
    //    {
    //        var productDto = new ProductDto(product.Id, product.Name, product.Description, product.Category, product.Price);
    //        productDtos.Add(productDto);
    //    }

    //    return productDtos;
    //}
}
