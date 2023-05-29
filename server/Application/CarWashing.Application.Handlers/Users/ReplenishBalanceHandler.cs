using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
using static CarWashing.Application.Contracts.Users.Commands.ReplenishBalance;

namespace CarWashing.Application.Handlers.Users;

internal class ReplenishBalanceHandler : IRequestHandler<Command> {
    private readonly IDatabaseContext _context;
    private readonly ICurrentUser _currentUser;

    public ReplenishBalanceHandler(IDatabaseContext context, ICurrentUser currentUser) {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken) {
        if (_currentUser.CanManageBalance() is false)
            throw AccessDeniedException.AnonymousUserHasNotAccess();

        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(request.UserId),
                cancellationToken) ?? throw EntityNotFoundException.For<User>(request.UserId);
        user.Money += request.Total;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
