namespace CarWashing.Application.Dto.Orders;

public record UserOrderDto(
    Guid Id,
    string Name,
    string Company,
    string Image,
    decimal Total,
    DateTime? OrderDate,
    uint? Count);
