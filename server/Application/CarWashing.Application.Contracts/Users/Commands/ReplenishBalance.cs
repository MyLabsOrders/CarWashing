using MediatR;

namespace CarWashing.Application.Contracts.Users.Commands;

internal static class ReplenishBalance {
    public record Command(Guid UserId, decimal Total) : IRequest;
}