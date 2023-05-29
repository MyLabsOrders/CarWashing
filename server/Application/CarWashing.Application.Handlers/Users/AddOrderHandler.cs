using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Domain.Core.Users;
using static CarWashing.Application.Contracts.Users.Commands.AddOrder;

namespace CarWashing.Application.Handlers.Users;

internal class AddOrderHandler : IRequestHandler<Command, Response> {
    private readonly IDatabaseContext _context;

    public AddOrderHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId), cancellationToken);

        if (user is not null) {
            if (request.Orders.ExceptBy(_context.Orders.Select(order => order.Id), dto => dto.OrderId).Any()) {
                throw new EntityNotFoundException($"Orders with ids {string.Join(", ", request.Orders.ExceptBy(_context.Orders.Select(order => order.Id), dto => dto.OrderId).Select(dto => dto.OrderId))} were not found.");
            }

            List<Order> orders = (await _context.Orders.ToListAsync(cancellationToken)).Join(
                    request.Orders,
                    order => order.Id,
                    dto => dto.OrderId,
                    (order, dto) => order).ToList();

            decimal totalPrice = 0;
            DateTime orderDate = DateTime.UtcNow;
            foreach ((Order order, (Guid OrderId, int Amount, int Days) dto) in orders.Join(
                        request.Orders,
                        order => order.Id,
                        dto => dto.OrderId,
                        (order, dto) => Tuple.Create(order, dto)
                        )) {
                Order newOrder = new(
                        id: Guid.NewGuid(),
                        user: user,
                        name: order.Name,
                        company: order.Company,
                        image: order.Image,
                        price: order.Price,
                        orderDate: orderDate) {
                    Amount = dto.Amount,
                    Period = dto.Days
                };
                _context.Orders.Add(newOrder);
                user.AddOrder(newOrder);
                totalPrice += newOrder.TotalPrice;
            }
            user.Money -= totalPrice;
            await _context.SaveChangesAsync(cancellationToken);

            return new Response(orderDate);
        }

        throw EntityNotFoundException.For<User>(request.UserId);
    }
}
