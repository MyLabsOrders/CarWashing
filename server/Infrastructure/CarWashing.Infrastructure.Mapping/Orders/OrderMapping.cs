using CarWashing.Application.Dto.Orders;
using CarWashing.Domain.Core.Orders;
using CarWashing.Infrastructure.Mapping.Users;

namespace CarWashing.Infrastructure.Mapping.Orders;

public static class OrderMapping {
    public static IEnumerable<UserOrderDto> ToDto(this IEnumerable<Order> orders) {
        return orders?.Select(order => order.ToUserOrderDto()) ?? Array.Empty<UserOrderDto>();
    }

    public static OrderDto ToDto(this Order order) {
        return new OrderDto(
            order.Id,
            order.User.ToDto(),
            order.Name,
            order.Company,
            order.Image.Value,
            order.Price,
            order.OrderDate,
            order.Amount,
            order.Period);
    }

    public static UserOrderDto ToUserOrderDto(this Order order) {
        return new UserOrderDto(
            order.Id,
            order.Name,
            order.Company,
            order.Image.Value,
            order.Price,
            order.OrderDate,
            order.Amount,
            order.Period);
    }
}
