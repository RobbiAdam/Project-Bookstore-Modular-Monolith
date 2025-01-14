using MediatR;

namespace Shared.Contract.CQRS;
public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
