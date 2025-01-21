using Shared.Pagination;

namespace Ordering.Orders.Features.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

public class GetOrdersQueryHandler(
    OrderDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        
        var totalCount = await context.Orders.LongCountAsync(cancellationToken);

        var orders = await context.Orders
            .AsNoTracking()
            .Include(x => x.OrderItems)
            .OrderBy(p => p.OrderName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var orderDtos = orders.Adapt<List<OrderDto>>();

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
                pageIndex,
                pageSize,
                totalCount,
                orderDtos));
    }
}
