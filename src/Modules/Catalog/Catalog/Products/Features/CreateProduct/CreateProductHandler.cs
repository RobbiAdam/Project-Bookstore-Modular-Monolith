namespace Catalog.Products.Features.CreateProduct;
public record CreateProductCommand(ProductDto Product) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Product description is required.");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Product category is required.");
        RuleFor(x => x.Product.Price)
            .NotEmpty().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");
            
    }
}

public class CreateProductCommandHandler(
    CatalogDbContext context) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateNewProduct(command.Product);

        context.Products.Add(product);

        await context.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            new Guid(),
            productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price
            );
        return product;
    }
}
