namespace Catalog.Contract.Products.Features.GetProductByIdQuery;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(ProductDto Product);