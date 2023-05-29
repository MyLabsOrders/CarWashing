using MediatR;

namespace CarWashing.Application.Contracts.Identity.Queries;

internal static class Login {
    public record Query(string Username, string Password) : IRequest<Response>;

    public record Response(Guid UserId, string Token);
}