using MediatR;
using TechRental.Application.Dto.Orders;

namespace TechRental.Application.Contracts.Orders.Queries;

internal class GetAllOrders {
    public record Query(int Page) : IRequest<Response>;

    public record Response(IEnumerable<UserOrderDto> Orders, int Page, int TotalPages);
}