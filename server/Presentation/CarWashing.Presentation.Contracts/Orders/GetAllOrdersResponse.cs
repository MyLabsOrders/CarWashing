using CarWashing.Application.Dto.Orders;

namespace CarWashing.Presentation.Contracts.Orders;

public record GetAllOrdersResponse(IEnumerable<UserOrderDto> Orders, int Page, int TotalPages);