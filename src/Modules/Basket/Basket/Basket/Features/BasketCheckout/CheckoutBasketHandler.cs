using Shared.Messaging.Events;
using System.Text.Json;

namespace Basket.Basket.Features.BasketCheckout;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketValidator()
    {
        RuleFor(x => x.BasketCheckout).NotNull().WithMessage("BasketCheckout is required");
        RuleFor(x => x.BasketCheckout.Username).NotNull().WithMessage("Username is required");
    }
}

public class CheckoutBasketHandler(BasketDbContext context) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var basket = await context.ShoppingCarts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(x => x.Username == command.BasketCheckout.Username, cancellationToken);

            if (basket is null)
                throw new BasketNotFoundException(command.BasketCheckout.Username);

            var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutIntegrationEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = typeof(BasketCheckoutIntegrationEvent).AssemblyQualifiedName!,
                Content = JsonSerializer.Serialize(eventMessage),
                OccuredOn = DateTime.UtcNow
            };

            context.OutboxMessages.Add(outboxMessage);

            context.ShoppingCarts.Remove(basket);

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new CheckoutBasketResult(true);

        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return new CheckoutBasketResult(false);
        }
    }
}
