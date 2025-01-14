namespace Basket.Basket.Features.CreateBasket;
public record CreateBasketCommand(ShoppingCartDto Cart) : ICommand<CreateBasketResult>;
public record CreateBasketResult(Guid Id);
public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("Username is required.");
    }
}

public class CreateBasketCommandHandler(
    IBasketRepository repository) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = CreateNewBasket(command.Cart);

        await repository.CreateBasket(cart, cancellationToken);
        
        return new CreateBasketResult(cart.Id);
    }

    private static ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        var newCart = ShoppingCart.Create(
            new Guid(), 
            shoppingCartDto.Username);

        shoppingCartDto.Items.ForEach(item =>
        {
            newCart.AddItem(
                item.ProductId,
                item.Quantity,
                item.Price,
                item.ProductName);
        });

        return newCart;
    }
}
