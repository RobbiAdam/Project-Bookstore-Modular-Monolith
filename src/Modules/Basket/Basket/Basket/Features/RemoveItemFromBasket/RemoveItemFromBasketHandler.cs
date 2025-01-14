namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketCommand(string Username, Guid ProductId) : ICommand<RemoveItemFromBasketResult>;

public record RemoveItemFromBasketResult(Guid Id);

public class RemoveItemFromBasketCommandValidator : AbstractValidator<RemoveItemFromBasketCommand>
{
    public RemoveItemFromBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
    }
}
public class RemoveItemFromBasketCommandHandler(
    IBasketRepository repository) : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
{
    public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
    {
        var cart = await repository.GetBasket(command.Username, false, cancellationToken);

        cart.RemoveItem(command.ProductId);

        await repository.SaveChangesAsync(command.Username, cancellationToken);
        return new RemoveItemFromBasketResult(cart.Id);
    }
}
