using MediatR;
using CarWashing.Application.Abstractions.Identity;
using static CarWashing.Application.Contracts.Identity.Queries.AuthorizeAdmin;

namespace CarWashing.Application.Handlers.Identity;

internal class AuthorizeAdminHandler : IRequestHandler<Query> {
    private readonly IAuthorizationService _authorizationService;

    public AuthorizeAdminHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }

    public async Task Handle(Query request, CancellationToken cancellationToken) {
        await _authorizationService.AuthorizeAdminAsync(request.Username, cancellationToken);
    }
}