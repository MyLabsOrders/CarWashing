using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.Domain.Core.Orders;
using CarWashing.Application.Contracts.Tools;
using CarWashing.Application.Dto.Orders;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Infrastructure.Mapping.Orders;
using static CarWashing.Application.Contracts.Orders.Queries.GetAllOrders;

namespace CarWashing.Application.Handlers.Orders;

internal class GetAllOrdersHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;
    private readonly int _pageCount;

    public GetAllOrdersHandler(IDatabaseContext context, PaginationConfiguration paginationConfiguration) {
        _context = context;
        _pageCount = paginationConfiguration.PageSize;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        DbSet<Order> query = _context.Orders;

        int ordersCount = await query.CountAsync(cancellationToken);
        int pageTotalCount = (int)Math.Ceiling((double)ordersCount / _pageCount);

        if (request.Page > pageTotalCount)
        {
            return new Response(Array.Empty<UserOrderDto>(), request.Page, pageTotalCount);
        }

        List<Order> orders = await query
            .Skip((request.Page - 1) * _pageCount)
            .Take(_pageCount)
            .ToListAsync(cancellationToken);

        return new Response(orders.ToDto(), request.Page, pageTotalCount);
    }
}
