using MediatR;

namespace Shared.Contract.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
