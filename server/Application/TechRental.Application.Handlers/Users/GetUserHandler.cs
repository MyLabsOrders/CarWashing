using MediatR;
using Microsoft.EntityFrameworkCore;
using TechRental.Application.Common.Exceptions;
using TechRental.DataAccess.Abstractions;
using TechRental.Domain.Common.Exceptions;
using TechRental.Domain.Core.Users;
using TechRental.Infrastructure.Mapping.Users;
using static TechRental.Application.Contracts.Users.Queries.GetUser;

namespace TechRental.Application.Handlers.Users;

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
