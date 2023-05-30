using MediatR;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Infrastructure.Mapping.Orders;
using static CarWashing.Application.Contracts.Orders.Commands.CreateOrder;

namespace CarWashing.Application.Handlers.Orders;

internal class CreateOrderHandler : IRequestHandler<Command, Response> {
    private readonly IDatabaseContext _context;
    private readonly ICurrentUser _currentUser;

    public CreateOrderHandler(IDatabaseContext context, ICurrentUser currentUser) {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {
        if (_currentUser.CanManageOrders() is false) {
            throw AccessDeniedException.AccessViolation();
        }

        Order order = new(Guid.NewGuid(),
                          user: null,
                          name: request.Name,
                          company: request.Company,
                          image: request.OrderImage,
                          price: request.Price,
                          orderDate: null);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(order.ToDto());
    }
}
