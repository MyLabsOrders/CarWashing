using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
using static CarWashing.Application.Contracts.Orders.Queries.GetCheque;

namespace CarWashing.Application.Handlers.Orders;

internal class GetChequeHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;
    private readonly ICurrentUser _currentUser;

    public GetChequeHandler(IDatabaseContext context, ICurrentUser currentUser) {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        User user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == _currentUser.Id,
                cancellationToken) ?? throw EntityNotFoundException.For<User>(_currentUser.Id);

        return (await _context.Orders
            .Where(order => order.UserId == _currentUser.Id && order.OrderDate == request.OrderDate)
            .ToListAsync(cancellationToken)).Any()
            ? new Response(new MemoryStream(new ChromePdfRenderer().RenderHtmlAsPdf(new StringBuilder(File.ReadAllText("Assets/Cheque.html"))
                .Replace(
                    "{Orders}",
                    string.Join('\n', (await _context.Orders
            .Where(order => order.UserId == _currentUser.Id && order.OrderDate == request.OrderDate)
            .ToListAsync(cancellationToken)).Select((order, i) => @$"
                        <p>{order.Name}</p>
                        <p>{i + 1} {order.Amount}x {order.Price} ={order.TotalPrice}B</p>"))
                )
                .Replace("{Total}", (await _context.Orders
            .Where(order => order.UserId == _currentUser.Id && order.OrderDate == request.OrderDate)
            .ToListAsync(cancellationToken)).Sum(order => order.TotalPrice).ToString())
                .ToString()).BinaryData))
            : throw new EntityNotFoundException("Order with this date is not found by this user.");
    }
}
