using MediatR;

namespace TechRental.Application.Contracts.Identity.Commands;

internal static class CreateAdmin {
    public record Command(string Username, string Password) : IRequest;
}