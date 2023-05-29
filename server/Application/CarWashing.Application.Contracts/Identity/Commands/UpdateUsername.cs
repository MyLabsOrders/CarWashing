using MediatR;

namespace CarWashing.Application.Contracts.Identity.Commands;

internal static class UpdateUsername {
    public record Command(string Username) : IRequest<Response>;

    public record Response(string Token);
}