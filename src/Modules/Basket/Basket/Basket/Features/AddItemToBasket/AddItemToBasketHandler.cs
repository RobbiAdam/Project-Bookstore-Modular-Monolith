using Catalog.Contract.Products.Features.GetProductByIdQuery;

namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketCommand(string Username, ShoppingCartItemDto Items) : ICommand<AddItemToBasketResult>;
public record AddItemToBasketResult(Guid BasketId);

public class AddItemToBasketCommandValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddItemToBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Items.ProductId).NotEmpty().WithMessage("Product id is required.");
        RuleFor(x => x.Items.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}

public class AddItemToBasketHandler(
    IBasketRepository repository, ISender sender) : ICommandHandler<AddItemToBasketCommand, AddItemToBasketResult>
{
    public async Task<AddItemToBasketResult> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.Username, false, cancellationToken);

        var result = await sender.Send(new GetProductByIdQuery(command.Items.ProductId), cancellationToken);

        cart.AddItem(
            command.Items.ProductId,
            command.Items.Quantity,
            result.Product.Price,
            result.Product.Name);

        await repository.SaveChangesAsync(command.Username ,cancellationToken);

        return new AddItemToBasketResult(cart.Id);

    }


}
