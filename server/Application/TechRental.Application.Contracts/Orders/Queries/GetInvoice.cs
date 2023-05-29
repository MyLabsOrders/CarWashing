using MediatR;
using TechRental.Application.Dto.Orders;

namespace TechRental.Application.Contracts.Orders.Queries;

internal static class GetInvoice {
    public record Query(DateTime OrderDate) : IRequest<Response>;

    public record Response(Stream Stream);
}
