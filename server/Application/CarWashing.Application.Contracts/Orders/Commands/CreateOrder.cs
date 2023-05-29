using MediatR;
using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Contracts.Orders.Commands;

internal static class CreateOrder {
    public record Command(string Name, string Company, string OrderImage, decimal Price) : IRequest<Response>;

    public record Response(OrderDto Order);
}
