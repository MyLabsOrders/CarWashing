using MediatR;

namespace TechRental.Application.Contracts.Users.Commands;

internal static class RemoveOrder {
    public record Command(Guid OrderId, Guid UserId) : IRequest;
}