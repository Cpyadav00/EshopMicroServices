
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOdersQuery(PaginationRequest PaginationRequest)
    :IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginatedResult<OrderDto> Order);