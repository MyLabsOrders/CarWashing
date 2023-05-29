using MediatR;
using TechRental.Application.Abstractions.Identity;
using TechRental.Application.Common;
using TechRental.Application.Common.Exceptions;
using TechRental.DataAccess.Abstractions;
using TechRental.Domain.Core.Users;
using static TechRental.Application.Contracts.Identity.Commands.CreateUserAccount;

namespace TechRental.Application.Handlers.Identity;

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
        if (_currentUser.CanCreateUserWithRole(request.RoleName) is false)
            throw AccessDeniedException.NotInRoleException();

        var response = await _authorizationService.CreateUserAsync(
                Guid.NewGuid(),
                request.Username,
                request.Password,
                request.RoleName,
                cancellationToken);

        var token = await _authorizationService.GetUserTokenAsync(response.Username, cancellationToken);

        return new Response(token);
    }
}