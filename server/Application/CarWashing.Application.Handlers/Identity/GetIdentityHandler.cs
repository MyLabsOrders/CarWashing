using MediatR;
using CarWashing.Application.Dto.Identity;
using CarWashing.Application.Abstractions.Identity;
using static CarWashing.Application.Contracts.Identity.Queries.GetIdentity;

namespace CarWashing.Application.Handlers.Identity;

internal class GetIdentityHandler : IRequestHandler<Query, Response> {
    private readonly IAuthorizationService _authorizationService;

    public GetIdentityHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        IdentityUserDto identityUser = await _authorizationService.GetUserByIdAsync(request.Id, cancellationToken);
        string identityRole = await _authorizationService.GetUserRoleAsync(request.Id, cancellationToken);

        return new Response(identityUser.Username, identityRole);
    }
}