

using Ordering.Application.Orders.Queries.GetOrdersByCustomer;
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetOdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex=query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount=await dbContext.Orders.LongCountAsync(cancellationToken);
        var orders=await dbContext.Orders
                .Include(o => o.OrderItems)
    .OrderBy(o => o.OrderName.Value)
    .Skip(pageSize*pageIndex)
    .Take(pageSize)
    .ToListAsync(cancellationToken);

        return new GetOrdersResult(
            new PaginatedResult<OrderDto>(
               pageIndex,
               pageSize,
               totalCount,
               orders.ToOrderDtoList()));
    }
}
