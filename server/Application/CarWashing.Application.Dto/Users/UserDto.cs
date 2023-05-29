using CarWashing.Application.Dto.Orders;

namespace CarWashing.Application.Dto.Users;

public record UserDto (
    Guid Id,
    string FirstName,
    string MiddleName,
    string LastName,
    string Image,
    DateOnly BirthDate,
    string Number,
    string Gender,
    bool IsActive,
    decimal Money,
    IEnumerable<UserOrderDto> Orders
);
