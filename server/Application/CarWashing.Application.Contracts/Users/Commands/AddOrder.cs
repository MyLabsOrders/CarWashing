using MediatR;

namespace CarWashing.Application.Contracts.Users.Commands;

internal static class AddOrder {
    public record Command(Guid UserId, IList<(Guid OrderId, uint Amount, uint Days)> Orders) : IRequest<Response>;
    public record Response(DateTime OrderTime);
}
