using MediatR;
using CarWashing.Application.Dto.Users;

namespace CarWashing.Application.Contracts.Users.Queries;

internal static class GetUser {
    public record Query(Guid UserId) : IRequest<Response>;

    public record Response(UserDto User);
}