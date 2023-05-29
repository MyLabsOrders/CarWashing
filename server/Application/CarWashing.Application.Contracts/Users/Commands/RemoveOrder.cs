using MediatR;

namespace CarWashing.Application.Contracts.Users.Commands;

internal static class RemoveOrder {
    public record Command(Guid OrderId, Guid UserId) : IRequest;
}