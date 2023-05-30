using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Domain.Core.Users;
using static CarWashing.Application.Contracts.Users.Commands.RemoveOrder;

namespace CarWashing.Application.Handlers.Users;

internal class RemoveOrderHandler : IRequestHandler<Command> {
    private readonly IDatabaseContext _context;

    public RemoveOrderHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken) {
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId), cancellationToken);
        Order? order = await _context.Orders.FirstOrDefaultAsync(x => x.Id.Equals(request.OrderId), cancellationToken);

        if (user is null) {
            throw EntityNotFoundException.For<User>(request.UserId);
        }

        if (order is null) {
            throw EntityNotFoundException.For<Order>(request.OrderId);
        }

        ProcessOperation(order);
        user.RemoveOrder(order);

        await _context.SaveChangesAsync(cancellationToken);
    }

    private static void ProcessOperation(Order order) {
        order.OrderDate = null;
        order.Amount = null;
    }
}
