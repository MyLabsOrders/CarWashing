using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.Application.Common.Exceptions;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
using CarWashing.Infrastructure.Mapping.Users;
using static CarWashing.Application.Contracts.Users.Queries.GetUser;

namespace CarWashing.Application.Handlers.Users;

internal class GetUserHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;

    public GetUserHandler(IDatabaseContext context) {
        _context = context;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        var user = await _context.Users.Include(x => x.Orders)
            .FirstAsync(x => x.Id.Equals(request.UserId), cancellationToken) ?? throw EntityNotFoundException.For<User>(request.UserId);
        return new Response(user.ToDto()!);
    }
}
