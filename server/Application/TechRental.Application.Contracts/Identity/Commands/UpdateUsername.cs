using MediatR;

namespace TechRental.Application.Contracts.Identity.Commands;

internal static class UpdateUsername {
    public record Command(string Username) : IRequest<Response>;

    public record Response(string Token);
}