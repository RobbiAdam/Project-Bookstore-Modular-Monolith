using Shared.Exceptions;

namespace Ordering.Orders.Exceptions;
internal class OrderNotFoundException(Guid orderId) : NotFoundException("Order", orderId)
{

}
