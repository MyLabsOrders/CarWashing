using MediatR;
using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Contracts.Orders.Queries;

internal static class GetOrder {
    public record Query(DateTime OrderTime) : IRequest<Response>;

    public record Response(IList<OrderDto> Orders);
}
