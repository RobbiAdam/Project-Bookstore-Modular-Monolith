namespace Catalog.Products.Features.UpdateProduct;
public record UpdateProductCommand(ProductDto Product) : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Product.Price)
            .NotEmpty().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than 0.");
    }
}

public class UpdateProductCommandHandler(
    CatalogDbContext context) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([command.Product.Id], cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product with id {command.Product.Id} not found");
        }

        UpdateProductWithNewValues(product, command.Product);

        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDto productDto)
    {
        product.Update(
            productDto.Name,
            productDto.Description,
            productDto.Category,
            productDto.Price);
    }
}
