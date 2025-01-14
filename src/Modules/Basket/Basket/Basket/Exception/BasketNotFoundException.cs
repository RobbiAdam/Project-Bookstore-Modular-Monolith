using Shared.Exceptions;

namespace Basket.Basket.Exception;
internal class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) : base("basket", username)
    {

    }
}
