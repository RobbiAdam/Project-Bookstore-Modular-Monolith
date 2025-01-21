namespace Ordering.Orders.Features.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteOrderCommandHandler(
    OrderDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .FindAsync([command.OrderId], cancellationToken: cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.OrderId);

        context.Orders.Remove(order);
        await context.SaveChangesAsync(cancellationToken);
        return new DeleteOrderResult(true);
    }
}
