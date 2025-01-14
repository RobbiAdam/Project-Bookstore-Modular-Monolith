namespace Catalog.Products.Features.DeleteProduct;

public record class DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record class DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty(). WithMessage("Id is required.");
    }
}

public class DeleteProductCommandHandler(
    CatalogDbContext context) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.FindAsync([command.Id], cancellationToken: cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product not found: {command.Id}");
        }

        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return new DeleteProductResult(true);
    }
}
