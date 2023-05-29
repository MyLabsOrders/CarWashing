using MediatR;
using CarWashing.Application.Abstractions.Identity;
using static CarWashing.Application.Contracts.Identity.Commands.UpdatePassword;

namespace CarWashing.Application.Handlers.Identity;

internal class UpdatePasswordHandler : IRequestHandler<Command, Response> {
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public UpdatePasswordHandler(ICurrentUser currentUser, IAuthorizationService authorizationService) {
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken) {
        var existingUser = await _authorizationService.UpdateUserPasswordAsync(
            _currentUser.Id,
            request.CurrentPassword,
            request.NewPassword,
            cancellationToken);

        var token = await _authorizationService.GetUserTokenAsync(existingUser.Username, cancellationToken);

        return new Response(token);
    }
}