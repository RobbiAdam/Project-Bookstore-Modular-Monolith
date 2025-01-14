namespace Basket.Basket.Features.UpdateItemPriceInBasket;

public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal NewPrice) : ICommand<UpdateItemPriceInBasketResult>;

public record UpdateItemPriceInBasketResult(bool IsSuccess);
public class UpdateItemPriceInBasketCommandHandler(
    BasketDbContext context) : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
    {
        var itemToUpdate = await context.ShoppingCartItems
            .Where(x => x.ProductId == command.ProductId)
            .ToListAsync();

        if (!itemToUpdate.Any())
        {
            return new UpdateItemPriceInBasketResult(false);
        }       

        foreach (var item in itemToUpdate)
        {
            item.UpdatePrice(command.NewPrice);
        }

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateItemPriceInBasketResult(true);
    }
}
