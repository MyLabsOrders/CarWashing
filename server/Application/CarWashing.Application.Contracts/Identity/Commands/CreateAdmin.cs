using MediatR;

namespace CarWashing.Application.Contracts.Identity.Commands;

internal static class CreateAdmin {
    public record Command(string Username, string Password) : IRequest;
}