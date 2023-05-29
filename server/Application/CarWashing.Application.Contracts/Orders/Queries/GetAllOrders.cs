using MediatR;
using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Contracts.Orders.Queries;

internal class GetAllOrders {
    public record Query(int Page) : IRequest<Response>;

    public record Response(IEnumerable<UserOrderDto> Orders, int Page, int TotalPages);
}