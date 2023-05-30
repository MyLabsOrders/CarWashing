using CarWashing.Application.Dto.Users;
using CarWashing.Domain.Core.Users;
using CarWashing.Infrastructure.Mapping.Orders;

namespace CarWashing.Infrastructure.Mapping.Users;

public static class UserMapping {
    public static IEnumerable<UserDto> ToDto(this IEnumerable<User> users) {
        return users.Select(x => x.ToDto()!);
    }

    public static UserDto? ToDto(this User? user) {
        return user is null
            ? null
            : new UserDto(
            user.Id,
            user.FirstName,
            user.MiddleName,
            user.LastName,
            user.Image,
            user.BirthDate,
            user.PhoneNumber.Value,
            user.Gender.ToString(),
            user.Status,
            user.Money,
            user.Orders.ToDto());
    }
}
