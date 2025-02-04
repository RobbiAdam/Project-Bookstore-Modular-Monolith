namespace Ordering.Orders.Features.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IQuery<GetOrderByIdResult>;

public record GetOrderByIdResult(QueryOrderDto Order);

public class GetOrderByIdHandler(
    OrderDbContext context) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
{
    public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var order = await context.Orders
            .AsNoTracking()
            .Include(x => x.OrderItems)
            .SingleOrDefaultAsync(p => p.Id == query.OrderId, cancellationToken);


        if(order == null ) 
            throw new OrderNotFoundException(query.OrderId);

        var orderDto = order.Adapt<QueryOrderDto>();

        return new GetOrderByIdResult(orderDto);
    }
}
