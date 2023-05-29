using TechRental.Application.Dto.Users;

namespace TechRental.Application.Dto.Orders;

public record OrderDto(
    Guid Id,
    UserDto? User,
    string Name,
    string Company,
    string Image,
    decimal Total,
    DateTime? OrderDate,
    int? Count,
    int? Days);
