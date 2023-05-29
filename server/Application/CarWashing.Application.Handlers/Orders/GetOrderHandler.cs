using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Infrastructure.Mapping.Orders;
using static CarWashing.Application.Contracts.Orders.Queries.GetOrder;

namespace CarWashing.Application.Handlers.Orders;

internal class GetOrderHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;

    public GetOrderHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        List<Order> orders = await _context.Orders
            .Where(order => order.OrderDate == request.OrderTime)
            .Include(order => order.User)
            .ToListAsync(cancellationToken);

        return !orders.Any()
            ? throw new EntityNotFoundException("Order with this date does not exist")
            : new Response(orders.Select(order => order.ToDto()).ToList());
    }
}
