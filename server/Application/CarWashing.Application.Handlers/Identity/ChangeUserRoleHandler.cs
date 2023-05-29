using MediatR;
using CarWashing.Application.Abstractions.Identity;
using CarWashing.Application.Common;
using CarWashing.Application.Common.Exceptions;
using static CarWashing.Application.Contracts.Identity.Commands.ChangeUserRole;

namespace CarWashing.Application.Handlers.Identity;

internal class ChangeUserRoleHandler : IRequestHandler<Command> {
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public ChangeUserRoleHandler(ICurrentUser currentUser, IAuthorizationService authorizationService) {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task Handle(Command request, CancellationToken cancellationToken) {
        var user = await _authorizationService.GetUserByNameAsync(request.Username, cancellationToken);
        var userRoleName = await _authorizationService.GetUserRoleAsync(user.Id, cancellationToken);

        if (_currentUser.CanChangeUserRole(userRoleName, request.UserRole) is false)
            throw AccessDeniedException.NotInRoleException();

        await _authorizationService.UpdateUserRoleAsync(user.Id, request.UserRole, cancellationToken);
    }
}