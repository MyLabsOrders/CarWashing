using CarWashing.Application.Dto.Users;

namespace CarWashing.Application.Dto.Orders;

public record OrderDto(
    Guid Id,
    UserDto? User,
    string Name,
    string Company,
    string Image,
    decimal Total,
    DateTime? OrderDate,
    uint? Count);
