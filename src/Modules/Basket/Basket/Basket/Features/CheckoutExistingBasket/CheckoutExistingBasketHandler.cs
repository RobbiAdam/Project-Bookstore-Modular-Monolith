using Shared.Messaging.Events;
using System.Text.Json;

namespace Basket.Basket.Features.CheckoutExistingBasket;

public record BasketCheckout(
    // Shipping and BillingAddress
    string FirstName,
    string LastName,
    string EmailAddress,
    string AddressLine,
    string Country,
    string State,
    string ZipCode,
    //Payment
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    int PaymentMethod);

public record CheckoutExistingBasketCommand(
    string Username, BasketCheckout BasketCheckout) : ICommand<CheckoutExistingBasketResult>;

public record CheckoutExistingBasketResult(bool IsSuccess);

public class CheckoutExistingBasketCommandHandler(
    BasketDbContext context)
    : ICommandHandler<CheckoutExistingBasketCommand, CheckoutExistingBasketResult>
{
    public async Task<CheckoutExistingBasketResult> Handle(CheckoutExistingBasketCommand command, CancellationToken cancellationToken)
    {
        await using var transaction =
            await context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var basket = await context.ShoppingCarts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);

            if (basket is null)
                throw new BasketNotFoundException(command.Username);

            var eventMessage = MappingBasketToEventMessage(command.BasketCheckout, basket);

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

            return new CheckoutExistingBasketResult(true);

        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            return new CheckoutExistingBasketResult(false);

        }
    }

    private BasketCheckoutIntegrationEvent MappingBasketToEventMessage(BasketCheckout basketCheckout, ShoppingCart cart)
    {
        return new BasketCheckoutIntegrationEvent
        {
            Username = cart.Username,
            TotalPrice = cart.TotalPrice,
            CustomerId = cart.Id,

            //Shipping address
            FirstName = basketCheckout.FirstName,
            LastName = basketCheckout.LastName,
            EmailAddress = basketCheckout.EmailAddress,
            AddressLine = basketCheckout.AddressLine,
            Country = basketCheckout.Country,
            State = basketCheckout.State,
            ZipCode = basketCheckout.ZipCode,

            //Payment
            CardName = basketCheckout.CardName,
            CardNumber = basketCheckout.CardNumber,
            Expiration = basketCheckout.Expiration,
            Cvv = basketCheckout.Cvv,
            PaymentMethod = basketCheckout.PaymentMethod
        };
    }

}
