using MediatR;

namespace CarWashing.Application.Contracts.Identity.Commands;

internal static class CreateUserAccount {
    public record Command(string Username, string Password, string RoleName) : IRequest<Response>;

    public record Response(string Token);
}