using MediatR;
using TechRental.Application.Abstractions.Identity;
using static TechRental.Application.Contracts.Identity.Queries.AuthorizeAdmin;

namespace TechRental.Application.Handlers.Identity;

internal class AuthorizeAdminHandler : IRequestHandler<Query> {
    private readonly IAuthorizationService _authorizationService;

    public AuthorizeAdminHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }

    public async Task Handle(Query request, CancellationToken cancellationToken) {
        await _authorizationService.AuthorizeAdminAsync(request.Username, cancellationToken);
    }
}