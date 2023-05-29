using MediatR;
using Microsoft.EntityFrameworkCore;
using TechRental.Application.Contracts.Tools;
using TechRental.Application.Dto.Orders;
using TechRental.DataAccess.Abstractions;
using TechRental.Infrastructure.Mapping.Orders;
using static TechRental.Application.Contracts.Orders.Queries.GetAllOrders;

namespace TechRental.Application.Handlers.Orders;

internal class GetAllOrdersHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;
    private readonly int _pageCount;

    public GetAllOrdersHandler(IDatabaseContext context, PaginationConfiguration paginationConfiguration) {
        _context = context;
        _pageCount = paginationConfiguration.PageSize;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        var query = _context.Orders;

        var ordersCount = await query.CountAsync(cancellationToken);
        var pageTotalCount = (int)Math.Ceiling((double)ordersCount / _pageCount);

        if (request.Page > pageTotalCount)
            return new Response(Array.Empty<UserOrderDto>(), request.Page, pageTotalCount);

        var orders = await query
            .Skip((request.Page - 1) * _pageCount)
            .Take(_pageCount)
            .ToListAsync(cancellationToken);

        return new Response(orders.ToDto(), request.Page, pageTotalCount);
    }
}
