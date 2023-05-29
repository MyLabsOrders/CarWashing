using MediatR;
using TechRental.Application.Dto.Users;

namespace TechRental.Application.Contracts.Users.Queries;

internal static class GetUser {
    public record Query(Guid UserId) : IRequest<Response>;

    public record Response(UserDto User);
}