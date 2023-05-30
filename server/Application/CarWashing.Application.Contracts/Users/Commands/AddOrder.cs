using MediatR;

namespace CarWashing.Application.Contracts.Users.Commands;

internal static class AddOrder {
    public record Command(Guid UserId, IList<(Guid OrderId, uint Amount)> Orders) : IRequest<Response>;
    public record Response(DateTime OrderTime);
}
