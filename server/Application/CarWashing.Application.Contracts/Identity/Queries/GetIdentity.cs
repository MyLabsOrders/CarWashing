using MediatR;

namespace CarWashing.Application.Contracts.Identity.Queries;

internal static class GetIdentity {
    public record Query(Guid Id) : IRequest<Response>;

    public record Response(string Username, string Role);
}