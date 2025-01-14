namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool Success);

public class DeleteBasketCommandHandler(
    IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.Username, cancellationToken);

        return new DeleteBasketResult(true);
    }
}
