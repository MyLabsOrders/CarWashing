using CarWashing.Application.Dto.Users;

namespace CarWashing.Presentation.Contracts.Users;

public record GetAllUsersResponse(IEnumerable<UserDto> Users, int Page, int TotalPages);