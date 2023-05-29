using MediatR;
using TechRental.Application.Abstractions.Identity;
using static TechRental.Application.Contracts.Identity.Queries.GetIdentity;

namespace TechRental.Application.Handlers.Identity;

internal class GetIdentityHandler : IRequestHandler<Query, Response> {
    private readonly IAuthorizationService _authorizationService;

    public GetIdentityHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        var identityUser = await _authorizationService.GetUserByIdAsync(request.Id, cancellationToken);
        var identityRole = await _authorizationService.GetUserRoleAsync(request.Id, cancellationToken);

        return new Response(identityUser.Username, identityRole);
    }
}