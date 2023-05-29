using MediatR;
using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Contracts.Orders.Queries;

internal static class GetCheque {
    public record Query(DateTime OrderDate) : IRequest<Response>;

    public record Response(Stream Stream);
}
