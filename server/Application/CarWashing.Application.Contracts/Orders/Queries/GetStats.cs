using MediatR;
using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Contracts.Orders.Queries;

internal static class GetStats {
    public record Query(DateTime From, DateTime To) : IRequest<Response>;

    public record Response(Stream Stream);
}
