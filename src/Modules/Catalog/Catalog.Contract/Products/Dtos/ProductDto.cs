namespace Catalog.Contract.Products.Dtos;
public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    List<string> Category,
    decimal Price);

