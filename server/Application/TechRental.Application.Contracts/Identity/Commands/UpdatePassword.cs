using MediatR;

namespace TechRental.Application.Contracts.Identity.Commands;

internal static class UpdatePassword {
    public record Command(string CurrentPassword, string NewPassword) : IRequest<Response>;

    public record Response(string Token);
}