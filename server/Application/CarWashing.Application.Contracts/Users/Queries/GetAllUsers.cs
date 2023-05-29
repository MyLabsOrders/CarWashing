using MediatR;
using CarWashing.Application.Dto.Users;

namespace CarWashing.Application.Contracts.Users.Queries;

internal class GetAllUsers {
    public record Query(int Page) : IRequest<Response>;

    public record Response(IEnumerable<UserDto> Users, int Page, int TotalPages);
}