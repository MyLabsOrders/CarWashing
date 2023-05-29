using MediatR;

namespace TechRental.Application.Contracts.Identity.Commands;

internal static class ChangeUserRole {
    public record Command(string Username, string UserRole) : IRequest;
}