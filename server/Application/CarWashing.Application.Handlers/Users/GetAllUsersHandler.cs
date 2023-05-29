using MediatR;
using Microsoft.EntityFrameworkCore;
using CarWashing.Application.Contracts.Tools;
using CarWashing.Application.Dto.Users;
using CarWashing.DataAccess.Abstractions;
using CarWashing.Infrastructure.Mapping.Users;
using static CarWashing.Application.Contracts.Users.Queries.GetAllUsers;

namespace CarWashing.Application.Handlers.Users;

internal class GetAllUsersHandler : IRequestHandler<Query, Response> {
    private readonly IDatabaseContext _context;
    private readonly int _pageCount;

    public GetAllUsersHandler(IDatabaseContext context, PaginationConfiguration paginationConfiguration) {
        _context = context;
        _pageCount = paginationConfiguration.PageSize;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        var query = _context.Users;

        var usersCount = await _context.Users.CountAsync(cancellationToken);
        var pageTotalCount = (int)Math.Ceiling((double)usersCount / _pageCount);

        if (request.Page > pageTotalCount)
            return new Response(Array.Empty<UserDto>(), request.Page, pageTotalCount);

        var users = await query
            .Include(x => x.Orders)
            .OrderBy(x => x.LastName)
            .Skip((request.Page - 1) * _pageCount)
            .Take(_pageCount)
            .ToListAsync(cancellationToken);

        return new Response(users.ToDto(), request.Page, pageTotalCount);
    }
}
