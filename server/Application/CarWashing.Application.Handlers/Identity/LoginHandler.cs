using MediatR;
using CarWashing.Application.Dto.Identity;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Domain.Common.Exceptions;
using static CarWashing.Application.Contracts.Identity.Queries.Login;

namespace CarWashing.Application.Handlers.Identity;

internal class LoginHandler : IRequestHandler<Query, Response> {
    private readonly IAuthorizationService _authorizationService;

    public LoginHandler(IAuthorizationService authorizationService) {
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken) {
        IdentityUserDto user = await _authorizationService.GetUserByNameAsync(request.Username, cancellationToken);

        bool passwordCorrect = await _authorizationService.CheckUserPasswordAsync(
            user.Id,
            request.Password,
            cancellationToken);

        if (passwordCorrect is false)
        {
            throw new UnauthorizedException("You are not authorized");
        }

        string token = await _authorizationService.GetUserTokenAsync(request.Username, cancellationToken);

        return new Response(user.Id, token);
    }
}
