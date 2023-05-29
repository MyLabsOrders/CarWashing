using MediatR;
using CarWashing.Application.Dto.Identity;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common.Exceptions;
using static CarWashing.Application.Contracts.Identity.Commands.CreateUserAccount;

namespace CarWashing.Application.Handlers.Identity;

internal class CreateUserAccountHandler : IRequestHandler<Command, Response> {
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public CreateUserAccountHandler(
        IAuthorizationService authorizationService,
        ICurrentUser currentUser) {
        _authorizationService = authorizationService;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {
        if (_currentUser.CanCreateUserWithRole(request.RoleName) is false) {
            throw AccessDeniedException.NotInRoleException();
        }

        IdentityUserDto response = await _authorizationService.CreateUserAsync(
                Guid.NewGuid(),
                request.Username,
                request.Password,
                request.RoleName,
                cancellationToken);

        string token = await _authorizationService.GetUserTokenAsync(response.Username, cancellationToken);

        return new Response(token);
    }
}